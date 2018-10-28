using Coma.Common;
using Coma.Common.Map.Item;
using Coma.Common.Message;
using Coma.Server.Model;
using Coma.Server.Model.Item;
using Coma.Server.Model.Item.Fonctions;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core.Module
{
    public class MapModule : BaseModule
    {
        private Random random;

        public MapModule()
            : base(1000)
        {
            random = new Random();
        }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.BodyPlayer != null || GameModel.Instance.SoulPlayer != null)
            {
                if (!GameModel.Instance.BodyCamInit && GameModel.Instance.BodyPlayer != null)
                {
                    var camMessage = new CameraMessage(GameModel.Instance.BodyMap.Start.X, GameModel.Instance.BodyMap.Start.Y);
                    GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, camMessage.ToString());
                    GameModel.Instance.BodyCamInit = true;
                }

                if (!GameModel.Instance.SoulCamInit && GameModel.Instance.SoulPlayer != null)
                {
                    var camMessage = new CameraMessage(GameModel.Instance.SoulMap.Start.X, GameModel.Instance.SoulMap.Start.Y);
                    GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, camMessage.ToString());
                    GameModel.Instance.SoulCamInit = true;
                }

                ResetTiles(PlayerType.BODY);
                ResetTiles(PlayerType.SOUL);

                UpdateTiles(PlayerType.BODY);
                UpdateTiles(PlayerType.SOUL);

                ResolveDestructions(PlayerType.BODY);
                ResolveDestructions(PlayerType.SOUL);

                if (GameModel.Instance.BodyPlayer != null)
                {
                    MapMessage bodyMessage = new MapMessage(GameModel.Instance.BodyMap.GetTiles());
                    GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, bodyMessage.ToString());
                }

                if (GameModel.Instance.SoulPlayer != null)
                {
                    MapMessage soulMessage = new MapMessage(GameModel.Instance.SoulMap.GetTiles());
                    GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, soulMessage.ToString());
                }
            }

            BankMessage bankMessage = new BankMessage(GameModel.Instance.Bank.Cells, GameModel.Instance.Bank.Nutrients, GameModel.Instance.Bank.Thoughts, GameModel.Instance.Bank.Ideas);

            if (GameModel.Instance.BodyPlayer != null)
            {
                GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, bankMessage.ToString());
            }
            if (GameModel.Instance.SoulPlayer != null)
            {
                GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, bankMessage.ToString());
            }
        }

        private void ResetTiles(PlayerType mapType)
        {
            var map = GameModel.Instance.GetMap(mapType);

            //reset
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    map.GetTiles()[i, j].Influence = 0;
                    map.GetTiles()[i, j].Contructable = false;
                    map.GetTiles()[i, j].Radiance = false;
                }
            }
        }

        private void UpdateTiles(PlayerType mapType)
        {
            var map = GameModel.Instance.GetMap(mapType);

            var listConsoCells = new List<Position>();
            var listConsoThoughts= new List<Position>();
            var listConsoNutrients = new List<Position>();
            var listConsoIdeas = new List<Position>();

            //execution fonctions
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    var tmppos = new Position(i, j);
                    if (tmppos.IsInMap(map.GetTiles().GetLength(1)))
                    {
                        FunctionInfo fonctions = TileItemFunctionInfo.Get(map.GetTiles()[i, j].Item.ItemType);
                        fonctions.MainFunction.Execute(mapType, tmppos);
                        fonctions.SecondaryFunction.Execute(mapType, tmppos);

                        if(TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceCellCostRate > 0)
                        {
                            listConsoCells.Add(tmppos);
                        }
                        if (TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceThoughtCostRate > 0)
                        {
                            listConsoThoughts.Add(tmppos);
                        }
                        if (TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceNutrientCostRate > 0)
                        {
                            listConsoNutrients.Add(tmppos);
                        }
                        if (TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceIdeaCostRate > 0)
                        {
                            listConsoIdeas.Add(tmppos);
                        }

                        //synergy
                        if (TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).SynergyMode != TileItemSynergyMode.NONE)
                        {
                            int nbSynergy = Synergy.CheckSynergyTrigger(map, tmppos, TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).SynergyMode, TileItemFunctionInfo.Get(map.GetTiles()[i, j].Item.ItemType).SynergyTrigger);
                            if (nbSynergy > 0)
                            {
                                fonctions.SynergyFunction.Execute(mapType, tmppos, nbSynergy);
                                map.GetTiles()[i, j].Item.Synergy = nbSynergy;
                            }
                        }
                        
                        //maintenance
                        GameModel.Instance.Bank.Cells -= TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceCellCostRate;
                        GameModel.Instance.Bank.Thoughts -= TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceThoughtCostRate;
                        GameModel.Instance.Bank.Nutrients -= TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceNutrientCostRate;
                        GameModel.Instance.Bank.Ideas -= TileItemInfo.Get(map.GetTiles()[i, j].Item.ItemType).MaintenanceIdeaCostRate;
                    }
                }
            }

            if (GameModel.Instance.Bank.Cells < 0 && listConsoCells.Count > 0)
            {
                var removePos = listConsoCells[random.Next(listConsoCells.Count)];
                map.GetTiles()[removePos.X, removePos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                GameModel.Instance.Bank.Cells = 0;
            }
            if (GameModel.Instance.Bank.Thoughts < 0 && listConsoThoughts.Count > 0)
            {
                var removePos = listConsoThoughts[random.Next(listConsoThoughts.Count)];
                map.GetTiles()[removePos.X, removePos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                GameModel.Instance.Bank.Thoughts = 0;
            }
            if (GameModel.Instance.Bank.Nutrients < 0 && listConsoNutrients.Count > 0)
            {
                var removePos = listConsoNutrients[random.Next(listConsoNutrients.Count)];
                map.GetTiles()[removePos.X, removePos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                GameModel.Instance.Bank.Nutrients = 0;
            }
            if (GameModel.Instance.Bank.Ideas < 0 && listConsoIdeas.Count > 0)
            {
                var removePos = listConsoIdeas[random.Next(listConsoIdeas.Count)];
                map.GetTiles()[removePos.X, removePos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                GameModel.Instance.Bank.Ideas = 0;
            }
        }

        private void ResolveDestructions(PlayerType mapType)
        {
            var map = GameModel.Instance.GetMap(mapType);
            
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    if (map.GetTiles()[i, j].Influence < 0)
                    {
                        //toi pas construire
                        map.GetTiles()[i, j].Contructable = false;

                        //
                        switch (map.GetTiles()[i, j].Item.ItemType)
                        {
                            case TileItemType.GENERATOR_SOUL:
                            case TileItemType.GENERATOR_BODY:
                            case TileItemType.HARVESTOR_BODY:
                            case TileItemType.HARVESTOR_SOUL:
                            case TileItemType.BUILD_AREA_BODY:
                            case TileItemType.BUILD_AREA_SOUL:
                            case TileItemType.RADIANCE_AREA_BODY:
                            case TileItemType.RADIANCE_AREA_SOUL:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                break;
                            case TileItemType.ORGAN:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_ORGAN);
                                break;
                            case TileItemType.FEELING:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_FEELING);
                                break;
                            default:
                                break;
                        }
                    }

                    if (map.GetTiles()[i, j].Influence > 0)
                    {
                        switch (map.GetTiles()[i, j].Item.ItemType)
                        {
                            case TileItemType.VIRUS:
                            case TileItemType.NIGHTMARE:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                break;
                            case TileItemType.CORRUPTED_ORGAN:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.ORGAN);
                                break;
                            case TileItemType.CORRUPTED_FEELING:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.FEELING);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
