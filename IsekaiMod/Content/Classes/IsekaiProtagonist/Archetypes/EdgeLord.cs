﻿using IsekaiMod.Extensions;
using IsekaiMod.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;

namespace IsekaiMod.Content.Classes.IsekaiProtagonist.Archetypes
{
    class EdgeLord
    {
        public static void Add()
        {
            // Archetype features
            var EdgeLordProficiencies = Resources.GetModBlueprint<BlueprintFeature>("EdgeLordProficiencies");
            var EdgeLordPlotArmor = Resources.GetModBlueprint<BlueprintFeature>("EdgeLordPlotArmor");
            var SupersonicCombat = Resources.GetModBlueprint<BlueprintFeature>("SupersonicCombat");
            var EdgeLordTraining = Resources.GetModBlueprint<BlueprintFeature>("EdgeLordTraining");
            var EdgeLordSignatureAttack = Resources.GetModBlueprint<BlueprintFeature>("EdgeLordSignatureAttack");
            var EdgeLordFastMovement = Resources.GetModBlueprint<BlueprintFeature>("EdgeLordFastMovement");
            var ExtraStrike1 = Resources.GetModBlueprint<BlueprintFeature>("ExtraStrike1");
            var ExtraStrike2 = Resources.GetModBlueprint<BlueprintFeature>("ExtraStrike2");
            var ExtraStrike3 = Resources.GetModBlueprint<BlueprintFeature>("ExtraStrike3");
            var ExtraStrike4 = Resources.GetModBlueprint<BlueprintFeature>("ExtraStrike4");
            var CripplingStrike = Resources.GetBlueprint<BlueprintFeature>("b696bd7cb38da194fa3404032483d1db");
            var DispellingAttack = Resources.GetBlueprint<BlueprintFeature>("1b92146b8a9830d4bb97ab694335fa7c");

            // Removed features
            var IsekaiProtagonistProficiencies = Resources.GetModBlueprint<BlueprintFeature>("IsekaiProtagonistProficiencies");
            var PlotArmor = Resources.GetModBlueprint<BlueprintFeature>("PlotArmor");
            var IsekaiFighterTraining = Resources.GetModBlueprint<BlueprintFeature>("IsekaiFighterTraining");
            var SignatureAttack = Resources.GetModBlueprint<BlueprintFeature>("SignatureAttack");
            var IsekaiFastMovement = Resources.GetModBlueprint<BlueprintFeature>("IsekaiFastMovement");
            var FriendlyAuraFeature = Resources.GetModBlueprint<BlueprintFeature>("FriendlyAuraFeature");

            var CharacterDevelopmentSelection1 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CharacterDevelopmentSelection1");
            var CharacterDevelopmentSelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CharacterDevelopmentSelection2");
            var CharacterDevelopmentSelection3 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CharacterDevelopmentSelection3");
            var CharacterDevelopmentSelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CharacterDevelopmentSelection4");

            // Archetype
            var EdgeLordArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("EdgeLordArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"EdgeLordArchetype.Name", "Edge Lord");
                bp.LocalizedDescription = Helpers.CreateString($"EdgeLordArchetype.Description", "After reincarnating into Golarion, some protagonists use their newfound abilities "
                    + "to look cool and stylish. Their attacks become flashy and myriad, moving so fast that side characters would be lucky to even see the afterimage.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"EdgeLordArchetype.Description", "After reincarnating into Golarion, some protagonists use their newfound abilities "
                    + "to look cool and stylish. Their attacks become flashy and myriad, moving so fast that side characters would be lucky to even see the afterimage.");
                bp.RemoveSpellbook = false;
                bp.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, IsekaiProtagonistProficiencies, PlotArmor),
                    Helpers.LevelEntry(3, CharacterDevelopmentSelection1, IsekaiFighterTraining),
                    Helpers.LevelEntry(6, SignatureAttack),
                    Helpers.LevelEntry(7, CharacterDevelopmentSelection2),
                    Helpers.LevelEntry(8, IsekaiFastMovement),
                    Helpers.LevelEntry(9, FriendlyAuraFeature),
                    Helpers.LevelEntry(13, CharacterDevelopmentSelection3),
                    Helpers.LevelEntry(19, CharacterDevelopmentSelection4),
                };
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, EdgeLordProficiencies, EdgeLordPlotArmor, SupersonicCombat),
                    Helpers.LevelEntry(2, EdgeLordTraining),
                    Helpers.LevelEntry(3, ExtraStrike1),
                    Helpers.LevelEntry(4, EdgeLordSignatureAttack),
                    Helpers.LevelEntry(7, ExtraStrike2, EdgeLordFastMovement),
                    Helpers.LevelEntry(8, CripplingStrike),
                    Helpers.LevelEntry(10, DispellingAttack),
                    Helpers.LevelEntry(13, ExtraStrike3),
                    Helpers.LevelEntry(19, ExtraStrike4),
                };
                bp.OverrideAttributeRecommendations = true;
                bp.RecommendedAttributes = new StatType[] { StatType.Dexterity, StatType.Charisma };
            });

            // Add Archetype to Class
            var IsekaiProtagonistClass = Resources.GetModBlueprint<BlueprintCharacterClass>("IsekaiProtagonistClass");
            IsekaiProtagonistClass.m_Archetypes = IsekaiProtagonistClass.m_Archetypes.AppendToArray(EdgeLordArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
