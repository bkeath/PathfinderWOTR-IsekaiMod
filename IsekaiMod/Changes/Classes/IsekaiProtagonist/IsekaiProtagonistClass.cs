﻿using IsekaiMod.Extensions;
using IsekaiMod.Utilities;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Utility;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;

namespace IsekaiMod.Changes.Classes.IsekaiProtagonist
{
    internal class IsekaiProtagonistClass
    {
        // Stat Progression
        private static readonly BlueprintStatProgression BaseAttackBonus = Resources.GetBlueprint<BlueprintStatProgression>("b3057560ffff3514299e8b93e7648a9d");
        private static readonly BlueprintStatProgression SavesProgression = Resources.GetBlueprint<BlueprintStatProgression>("ff4662bde9e75f145853417313842751");

        // Proficiencies
        private static readonly BlueprintFeature LightArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("6d3728d4e9c9898458fe5e9532951132");
        private static readonly BlueprintFeature MediumArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("46f4fb320f35704488ba3d513397789d");
        private static readonly BlueprintFeature HeavyArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("1b0f68188dcc435429fb87a022239681");
        private static readonly BlueprintFeature SimpleWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("e70ecf1ed95ca2f40b754f1adb22bbdd");
        private static readonly BlueprintFeature MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
        private static readonly BlueprintFeature ShieldsProficiency = Resources.GetBlueprint<BlueprintFeature>("cb8686e7357a68c42bdd9d4e65334633");
        private static readonly BlueprintFeature TowerShieldProficiency = Resources.GetBlueprint<BlueprintFeature>("6105f450bb2acbd458d277e71e19d835");

        // Mythic Classes
        private static readonly BlueprintCharacterClass MythicCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintCharacterClass MythicStartingClass = Resources.GetBlueprint<BlueprintCharacterClass>("247aa787806d5da4f89cfc3dff0b217f");
        private static readonly BlueprintCharacterClass AeonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("15a85e67b7d69554cab9ed5830d0268e");
        private static readonly BlueprintCharacterClass AngelMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("a5a9fe8f663d701488bd1db8ea40484e");
        private static readonly BlueprintCharacterClass AzataMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("9a3b2c63afa79744cbca46bea0da9a16");
        private static readonly BlueprintCharacterClass DemonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("8e19495ea576a8641964102d177e34b7");
        private static readonly BlueprintCharacterClass LichMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("5d501618a28bdc24c80007a5c937dcb7");
        private static readonly BlueprintCharacterClass DevilMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("211f49705f478b3468db6daa802452a2");
        private static readonly BlueprintCharacterClass SwarmThatWalksClass = Resources.GetBlueprint<BlueprintCharacterClass>("5295b8e13c2303f4c88bdb3d7760a757");

        // Mythic Ability Selection
        private static readonly BlueprintFeatureSelection MythicAbilitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");


        // Abilities
        private static readonly BlueprintAbility ResistAcid = Resources.GetBlueprint<BlueprintAbility>("fedc77de9b7aad54ebcc43b4daf8decd");
        private static readonly BlueprintAbility ResistCold = Resources.GetBlueprint<BlueprintAbility>("5368cecec375e1845ae07f48cdc09dd1");
        private static readonly BlueprintAbility ResistElectricity = Resources.GetBlueprint<BlueprintAbility>("90987584f54ab7a459c56c2d2f22cee2");
        private static readonly BlueprintAbility ResistFire = Resources.GetBlueprint<BlueprintAbility>("ddfb4ac970225f34dbff98a10a4a8844");
        private static readonly BlueprintAbility ResistSonic = Resources.GetBlueprint<BlueprintAbility>("8d3b10f92387c84429ced317b06ad001");
        private static readonly BlueprintAbility DeathWard = Resources.GetBlueprint<BlueprintAbility>("0413915f355a38146bc6ad40cdf27b3f");
        private static readonly BlueprintAbility MageArmor = Resources.GetBlueprint<BlueprintAbility>("9e1ad5d6f87d19e4d8883d63a6e35568");

        public static void AddIsekaiProtagonistClass()
        {
            // TODO: character development feats should initially give energy resistance and then finally give immunity at a certain level.
            // TODO: debug harem magnet, (harem magnet buff is not removed on attack in UI), (harem magnet source stacks and is not removed in UI)
            // TODO: feature selection, rename "Exceptional feats" to "Hidden powers" or "character development feats", also should probably be more than just immunities
            // TODO: create "plot armor" feature group
            // TODO: create feature called "deus ex machina" that gives spell resistance
            // TODO: Add IgnoreDamageReductionOnAttack

            // TODO: Add a "hexagon-player" default build
            // TODO: Add MythicAbilitySelection ability
            // TODO: Add mythic spellbook merging

            // TODO: Add archetypes, Archetype ideas: God Emporer, Edge Lord
            // TODO: Add custom equipment

            var BasicFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
            var EdictOfImpenetrableFortress = Resources.GetBlueprint<BlueprintAbility>("d7741c08ccf699e4a8a8f8ab2ed345f8");

            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var MonkClass = Resources.GetBlueprint<BlueprintCharacterClass>("e8f21e5b58e0569468e420ebea456124");

            // Spellbook
            var IsekaiProtagonistSpellList = Resources.GetModBlueprint<BlueprintSpellList>("IsekaiProtagonistSpellList");
            var IsekaiProtagonistSpellsPerDay = Helpers.CreateBlueprint<BlueprintSpellsTable>("IsekaiProtagonistSpellsPerDay", bp => {
                bp.Levels = new SpellsLevelEntry[29] {
                    new SpellsLevelEntry() { Count = new int[] { } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 15 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20, 2 } }
                };
            });
            var IsekaiProtagonistSpellsKnown = Helpers.CreateBlueprint<BlueprintSpellsTable>("IsekaiProtagonistSpellsKnown", bp => {
                bp.Levels = new SpellsLevelEntry[21] {
                    new SpellsLevelEntry() { Count = new int[] { } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 15, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 10, 5 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 15, 10 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 15 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } },
                    new SpellsLevelEntry() { Count = new int[] { 0, 20, 20, 20, 20, 20, 20, 20, 20, 20 } }
                };
            });
            var IsekaiProtagonistSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("IsekaiProtagonistSpellbook", bp => {
                bp.Name = Helpers.CreateString("$IsekaiProtagonistSpellbook.Name", "Isekai Protagonist");
                bp.Spontaneous = true;
                bp.CastingAttribute = StatType.Charisma;
                bp.CantripsType = CantripsType.Cantrips;
                bp.IsArcane = true;
                bp.IsArcanist = false;
                bp.m_SpellsPerDay = IsekaiProtagonistSpellsPerDay.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellsKnown = IsekaiProtagonistSpellsKnown.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = IsekaiProtagonistSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_SpellSlots = null;
                bp.SpellsPerLevel = 0;
                bp.AllSpellsKnown = false;
                bp.CanCopyScrolls = false;

                // Mythic spellbook related
                bp.IsMythic = false;
                bp.m_MythicSpellList = null;

                // These relate to special spell slots (like wizard's favourite school spell slots or shaman's spirit magic slots)
                bp.HasSpecialSpellList = false;
                bp.SpecialSpellListName = new Kingmaker.Localization.LocalizedString();
            });
            // Progression
            var IsekaiProtagonistProgression = Helpers.CreateBlueprint<BlueprintProgression>("IsekaiProtagonistProgression", bp => {
                bp.SetName("");
                bp.SetDescription(
                    "Isekai protagonists are otherworldly entities who have been reincarnated into the world of Golarion with extraordinary abilities. " +
                    "As their story progresses, they gain more unexplained and overpowered abilities to overcome every challenge they face.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_FeaturesRankIncrease = null;
            });
            // Main Class
            var IsekaiProtagonistClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("IsekaiProtagonistClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"IsekaiProtagonistClass.Name", "Isekai Protagonist");
                bp.LocalizedDescription = Helpers.CreateString($"IsekaiProtagonistClass.Description", "Isekai protagonists are skilled in both martial prowess and magical ability. " +
                    "They are able to cast spells while wearing any armor or shield.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"IsekaiProtagonistClass.Description",
                    "Isekai protagonists are otherworldly entities who have been reincarnated into the world of Golarion with extraordinary abilities. " +
                    "As their story progresses, they gain more unexplained and overpowered abilities to overcome every challenge they face.");
                bp.HitDie = DiceType.D12;
                bp.m_BaseAttackBonus = BaseAttackBonus.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = SavesProgression.ToReference<BlueprintStatProgressionReference>();
                bp.m_ReflexSave = SavesProgression.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = SavesProgression.ToReference<BlueprintStatProgressionReference>();
                bp.m_Progression = IsekaiProtagonistProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = 1;
                bp.RecommendedAttributes = new StatType[] { StatType.Strength, StatType.Charisma};
                bp.NotRecommendedAttributes = new StatType[] { StatType.Constitution };
                bp.m_Spellbook = IsekaiProtagonistSpellbook.ToReference<BlueprintSpellbookReference>();
                bp.m_EquipmentEntities = new KingmakerEquipmentEntityReference[] { };
                bp.m_StartingItems = new BlueprintItemReference[] { };
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[] { };
                bp.SkillPoints = 0;
                bp.ClassSkills = new StatType[11] {
                    StatType.SkillAthletics,
                    StatType.SkillMobility,
                    StatType.SkillThievery,
                    StatType.SkillStealth,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillLoreNature,
                    StatType.SkillLoreReligion,
                    StatType.SkillPerception,
                    StatType.SkillPersuasion,
                    StatType.SkillUseMagicDevice
                };
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = true;
                bp.StartingGold = 69420;
                bp.PrimaryColor = 9;
                bp.SecondaryColor = 9;
                bp.MaleEquipmentEntities = MonkClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = MonkClass.FemaleEquipmentEntities;
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
            });
            IsekaiProtagonistProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = IsekaiProtagonistClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };
            IsekaiProtagonistSpellbook.m_CharacterClass = IsekaiProtagonistClass.ToReference<BlueprintCharacterClassReference>();

            //// Class Features
            // Proficiencies
            var IsekaiProtagonistProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("IsekaiProtagonistProficiencies", bp => {
                bp.SetName("Isekai Protagonist Proficiences");
                bp.SetDescription("Isekai Protagonists are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g} and with all armor (heavy, light, and medium) and shields (including tower shields). They can cast {g|Encyclopedia:Spell}spells{/g} from this class while wearing armor and shields (including tower shields) without incurring the normal {g|Encyclopedia:Spell_Fail_Chance}arcane spell failure chance{/g}, but they incur the normal arcane spell failure chance for arcane spells received from other classes.");
                bp.m_Icon = null;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LightArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        MediumArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        HeavyArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        SimpleWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        MartialWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        ShieldsProficiency.ToReference<BlueprintUnitFactReference>(),
                        TowerShieldProficiency.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.AddComponent<ArcaneArmorProficiency>(c => {
                    c.Armor = new ArmorProficiencyGroup[] {
                        ArmorProficiencyGroup.Light,
                        ArmorProficiencyGroup.Medium,
                        ArmorProficiencyGroup.Heavy,
                        ArmorProficiencyGroup.Buckler,
                        ArmorProficiencyGroup.LightShield,
                        ArmorProficiencyGroup.HeavyShield,
                        ArmorProficiencyGroup.TowerShield
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            // Cantrips (May need to change this in the future since it doesn't actually add the cantrips)
            var IsekaiProtagonistCantripsFeature = Helpers.CreateBlueprint<BlueprintFeature>("IsekaiProtagonistCantripsFeature", bp => {
                bp.SetName("Cantrips");
                bp.SetDescription("Isekai Protagonists can cast a number of {g|Encyclopedia:Cantrips_Orisons}cantrips{/g}, or 0-level {g|Encyclopedia:Spell}spells{/g}. These spells are cast like any other spell, but they are not expended when cast and may be used again.");
                bp.m_Icon = null;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[]
                    {
                        IsekaiProtagonist.IsekaiProtagonistSpellList.MageLightAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.JoltAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.DisruptUndeadAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.AcidSplashAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.DismissAreaEffectAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.DazeAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.TouchOfFatigueAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.FlareAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.RayOfFrostAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.ResistanceAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.DivineZapAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.GuidanceAbility.ToReference<BlueprintUnitFactReference>(),
                        IsekaiProtagonist.IsekaiProtagonistSpellList.VirtueAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            // Bonus Feats
            var IsekaiProtagonistBonusFeatSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("IsekaiProtagonistBonusFeatSelection", bp => {
                bp.SetName("Bonus Feat");
                bp.SetDescription("At 1st level, and at every even level thereafter, Isekai Protagonists gain a bonus {g|Encyclopedia:Feat}feat{/g} in addition to those gained from normal advancement.");
                bp.m_Icon = null;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.Feat;
                bp.Group2 = FeatureGroup.TricksterFeat;
                bp.m_AllFeatures = BasicFeatSelection.m_AllFeatures;
            });
            // Sneak Attack
            var SneakAttack = Resources.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87");
            // Plot Armor
            var PlotArmor = Helpers.CreateBlueprint<BlueprintFeature>("PlotArmor", bp => {
                bp.SetName("Plot Armor");
                bp.SetDescription("Isekai Protagonists gain a luck bonus to {g|Encyclopedia:Armor_Class}AC{/g} and all {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to their character level.");
                bp.m_DescriptionShort = Helpers.CreateString("PlotArmor.DescriptionShort", "Isekai Protagonists gain a luck bonus to {g|Encyclopedia:Armor_Class}AC{/g} and all {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to their character level.");
                bp.m_Icon = EdictOfImpenetrableFortress.m_Icon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                });
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
            });
            var UncannyDodge = Resources.GetBlueprint<BlueprintFeature>("3c08d842e802c3e4eb19d15496145709");
            var ImprovedUncannyDodge = Resources.GetBlueprint<BlueprintFeature>("485a18c05792521459c7d06c63128c79");
            var Evasion = Resources.GetBlueprint<BlueprintFeature>("576933720c440aa4d8d42b0c54b77e80");
            var ImprovedEvasion = Resources.GetBlueprint<BlueprintFeature>("ce96af454a6137d47b9c6a1e02e66803");
            // Capstone
            var Icon_TrueMainCharacter = AssetLoader.LoadInternal("Features", "ICON_TRUE_MAIN_CHARACTER.png");
            var TrueMainCharacter = Helpers.CreateBlueprint<BlueprintFeature>("TrueMainCharacter", bp => {
                bp.SetName("True Main Character");
                bp.SetDescription("You are the main character of this world. Your attacks ignore {g|Encyclopedia:Damage_Reduction}damage reduction{/g} and immunity to {g|Encyclopedia:Critical}critical hits{/g}. Your critical threats are also automatically confirmed. The {g|Encyclopedia:Spell}spells{/g} you cast ignore {g|Encyclopedia:Spell_Resistance}spell resistance{/g} and spell immunity.");
                bp.m_Icon = Icon_TrueMainCharacter;
                bp.AddComponent<IgnoreSpellImmunity>();
                bp.AddComponent<IgnoreSpellResistanceForSpells>();
                bp.AddComponent<IgnoreDamageReductionOnAttack>();
                bp.AddComponent<IgnoreCritImmunity>();
                bp.AddComponent<InitiatorCritAutoconfirm>();
                bp.IsClassFeature = true;
            });
            // Harem Magnet
            var Icon_Harem = AssetLoader.LoadInternal("Abilities", "ICON_HAREM.png");
            var HaremMagnetEffect = Helpers.CreateBlueprint<BlueprintBuff>("HaremMagnetEffect", bp => {
                bp.SetName("Fascinated");
                bp.SetDescription("This creature is Fascinated by the Protagonist and can take no actions unless attacked.");
                bp.m_Icon = Icon_Harem;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.FxOnStart = new PrefabLink() { AssetId = "396af91a93f6e2b468f5fa1a944fae8a" };
                bp.AddComponent<AddCondition>(c => { c.Condition = UnitCondition.Dazed; });
                bp.AddComponent<AddIncomingDamageTrigger>(c => {
                    c.TriggerOnStatDamageOrEnergyDrain = true;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionSpawnFx() {
                            PrefabLink = new PrefabLink() {
                                AssetId = "28b3cd92c1fdc194d9ee1e378c23be6b"
                            }
                        });
                });
            });
            var HaremMagnetSource = Helpers.CreateBlueprint<BlueprintBuff>("HaremMagnetSource", bp => {
                bp.SetName("Harem Magnet");
                bp.SetDescription("Enemies within 60 feet who fails a {g|Encyclopedia:DC}DC{/g} 50 {g|Encyclopedia:Saving_Throw}Will save{/g} loses any immunity to mind-affecting effects, charm effects, and compulsion effects, and becomes fascinated by you for {g|Encyclopedia:Dice}5d4{/g} {g|Encyclopedia:Combat_Round}rounds{/g}. A creature affected by a mind-affecting effect while within this aura remains affected even after leaving the Isekai Protagonist's presence. Creatures that succeed at this saving throw are immune to this ability for 24 hours.");
                bp.m_Icon = Icon_Harem;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var HaremMagnetImmunity = Helpers.CreateBlueprint<BlueprintBuff>("HaremMagnetImmunity", bp => {
                bp.SetName("Harem Magnet Immunity");
                bp.SetDescription("This creature is immune to the Protagonist's Harem Magnet for 24 hours.");
                bp.m_Icon = Icon_Harem;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.AddComponent<IsPositiveEffect>();
            });
            var HaremMagnetBuff = Helpers.CreateBlueprint<BlueprintBuff>("HaremMagnetBuff", bp => {
                bp.SetName("Fascinated");
                bp.SetDescription("This creature is Fascinated by the Protagonist and can take no actions unless attacked.");
                bp.m_Icon = Icon_Harem;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionSpawnFx()
                        {
                            PrefabLink = new PrefabLink()
                            {
                                AssetId = "28b3cd92c1fdc194d9ee1e378c23be6b"
                            }
                        },
                        new ContextActionApplyBuff()
                        {
                            m_Buff = HaremMagnetEffect.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                            },
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true
                        });
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff()
                        {
                            m_Buff = HaremMagnetEffect.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                });
                bp.AddComponent<AddIncomingDamageTrigger>(c => {
                    c.TriggerOnStatDamageOrEnergyDrain = true;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
            });
            var HaremMagnetAreaEffect = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("HaremMagnetAreaEffect", bp => {
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.StatType = StatType.Charisma;
                });
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.Round = Helpers.CreateActionList(
                        new Conditional()
                        {
                            ConditionsChecker = new ConditionsChecker()
                            {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = true,
                                        m_Fact = HaremMagnetBuff.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionHasFact() {
                                        Not = true,
                                        m_Fact = HaremMagnetSource.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionHasFact() {
                                        Not = true,
                                        m_Fact = HaremMagnetImmunity.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                    Type = SavingThrowType.Will,
                                    UseDCFromContextSavingThrow = true,
                                    CustomDC = 50,
                                    HasCustomDC = true,
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(
                                                new ContextActionApplyBuff()
                                                {
                                                    Permanent = false,
                                                    m_Buff = HaremMagnetImmunity.ToReference<BlueprintBuffReference>(),
                                                    DurationValue = new ContextDurationValue()
                                                    {
                                                        Rate = DurationRate.Hours,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = new ContextValue()
                                                        {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 24,
                                                            ValueRank = AbilityRankType.Default
                                                        },
                                                        m_IsExtendable = true
                                                    },
                                                    DurationSeconds = 0,
                                                    IsFromSpell = false,
                                                    ToCaster = false,
                                                    AsChild = true,
                                                    SameDuration = false,
                                                    NotLinkToAreaEffect = true
                                                }),
                                            Failed = Helpers.CreateActionList(
                                                new ContextActionApplyBuff()
                                                {
                                                    Permanent = false,
                                                    m_Buff = HaremMagnetBuff.ToReference<BlueprintBuffReference>(),
                                                    DurationValue = new ContextDurationValue()
                                                    {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.D4,
                                                        DiceCountValue = 5,
                                                        BonusValue = new ContextValue(),
                                                        m_IsExtendable = true
                                                    },
                                                    DurationSeconds = 0,
                                                    IsFromSpell = false,
                                                    ToCaster = false,
                                                    AsChild = true,
                                                    SameDuration = false,
                                                    NotLinkToAreaEffect = false
                                                })
                                        }
                                    )
                                }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.AggroEnemies = false;
                bp.AffectEnemies = true;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.Fx = null;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 60 };
            });
            HaremMagnetSource.AddComponent<AddAreaEffect>(c => {
                c.m_AreaEffect = HaremMagnetAreaEffect.ToReference<BlueprintAbilityAreaEffectReference>();
            });
            var HaremMagnetActivatableAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("HaremMagnetActivatableAbility", bp => {
                bp.SetName("Harem Magnet");
                bp.SetDescription("As a {g|Encyclopedia:Free_Action}free action{/g}, enemies within 60 feet who fails a {g|Encyclopedia:DC}DC{/g} 50 {g|Encyclopedia:Saving_Throw}Will save{/g} loses any immunity to mind-affecting effects, charm effects, and compulsion effects, and becomes fascinated by the Isekai Protagonist for {g|Encyclopedia:Dice}5d4{/g} {g|Encyclopedia:Combat_Round}rounds{/g}. A creature affected by a mind-affecting effect while within this aura remains affected even after leaving the Isekai Protagonist's presence. Creatures that succeed at this saving throw are immune to this ability for 24 hours.");
                bp.m_Icon = Icon_Harem;
                bp.m_Buff = HaremMagnetSource.ToReference<BlueprintBuffReference>();
            });
            var HaremMagnetFeature = Helpers.CreateBlueprint<BlueprintFeature>("HaremMagnetFeature", bp => {
                bp.SetName("Harem Magnet");
                bp.SetDescription("At 17th Level, the Isekai Protagonist gains the ability to attract anyone. As a {g|Encyclopedia:Free_Action}free action{/g}, enemies within 60 feet who fails a {g|Encyclopedia:DC}DC{/g} 50 {g|Encyclopedia:Saving_Throw}Will save{/g} loses any immunity to mind-affecting effects, charm effects, and compulsion effects, and becomes fascinated by the Isekai Protagonist for {g|Encyclopedia:Dice}5d4{/g} {g|Encyclopedia:Combat_Round}rounds{/g}. A creature affected by a mind-affecting effect while within this aura remains affected even after leaving the Isekai Protagonist's presence. Creatures that succeed at this saving throw are immune to this ability for 24 hours.");
                bp.m_Icon = Icon_Harem;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { HaremMagnetActivatableAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            // Otherworldly Stamina
            var Icon_Stamina = AssetLoader.LoadInternal("Features", "ICON_STAMINA.png");
            var OtherworldlyStamina = Helpers.CreateBlueprint<BlueprintFeature>("OtherworldlyStamina", bp => {
                bp.SetName("Otherworldly Stamina");
                bp.SetDescription("At 17th Level, the Isekai Protagonist becomes immune to fatigue and exhaustion conditions from spells and spell-like abilities.");
                bp.m_Icon = Icon_Harem;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Fatigued;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Exhausted;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Fatigue | SpellDescriptor.Exhausted;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Fatigue | SpellDescriptor.Exhausted;
                });
            });

            // Character development feats
            var Icon_CharacterDevelopment_1 = AssetLoader.LoadInternal("Features", "ICON_POSITIVE_SHIELD.png");
            var Icon_CharacterDevelopment_2 = AssetLoader.LoadInternal("Features", "ICON_NEGATIVE_SHIELD.png");
            var ImmunityToCriticalHitsFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToCriticalHitsFeat", bp => {
                bp.SetName("Immunity to Critical hits");
                bp.SetDescription("You gain immunity to {g|Encyclopedia:Critical}critical hits{/g}.");
                bp.m_Icon = MageArmor.m_Icon;
                bp.AddComponent<AddImmunityToCriticalHits>();
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToCriticalSneakFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToCriticalSneakFeat", bp => {
                bp.SetName("Immunity to Sneak attack damage");
                bp.SetDescription("You gain immunity to sneak attack damage.");
                bp.m_Icon = MageArmor.m_Icon;
                bp.AddComponent<AddImmunityToPrecisionDamage>();
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToAbilityScoreDamageFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToAbilityScoreDamageFeat", bp => {
                bp.SetName("Immunity to Ability score damage");
                bp.SetDescription("You gain immunity to {g|Encyclopedia:Ability_Scores}ability score{/g} {g|Encyclopedia:Damage}damage{/g}");
                bp.m_Icon = DeathWard.m_Icon;
                bp.AddComponent<AddImmunityToAbilityScoreDamage>();
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToEnergyDrainFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToEnergyDrainFeat", bp => {
                bp.SetName("Immunity to Energy drain");
                bp.SetDescription("You gain immunity to energy drain.");
                bp.m_Icon = DeathWard.m_Icon;
                bp.AddComponent<AddImmunityToEnergyDrain>();
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToAcidFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToAcidFeat", bp => {
                bp.SetName("Acid Immunity");
                bp.SetDescription("You gain immunity to Acid.");
                bp.m_Icon = ResistAcid.m_Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Acid;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Acid;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToColdFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToColdFeat", bp => {
                bp.SetName("Cold Immunity");
                bp.SetDescription("You gain immunity to Cold.");
                bp.m_Icon = ResistCold.m_Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Cold;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Cold;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Cold;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToElectricityFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToElectricityFeat", bp => {
                bp.SetName("Electricity Immunity");
                bp.SetDescription("You gain immunity to Electricity.");
                bp.m_Icon = ResistElectricity.m_Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Electricity;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Electricity;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Electricity;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToFireFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToFireFeat", bp => {
                bp.SetName("Fire Immunity");
                bp.SetDescription("You gain immunity to Fire.");
                bp.m_Icon = ResistFire.m_Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Fire;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToSonicFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToSonicFeat", bp => {
                bp.SetName("Sonic Immunity");
                bp.SetDescription("You gain immunity to Sonic.");
                bp.m_Icon = ResistSonic.m_Icon;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Sonic;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Sonic;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Sonic;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToHolyFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToHolyFeat", bp => {
                bp.SetName("Holy Immunity");
                bp.SetDescription("You gain immunity to Holy damage.");
                bp.m_Icon = Icon_CharacterDevelopment_1;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Holy;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToUnholyFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToUnholyFeat", bp => {
                bp.SetName("Unholy Immunity");
                bp.SetDescription("You gain immunity to Unholy damage.");
                bp.m_Icon = Icon_CharacterDevelopment_2;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.Unholy;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToPositiveFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToPositiveFeat", bp => {
                bp.SetName("Positive energy Immunity");
                bp.SetDescription("You gain immunity to Positive energy.");
                bp.m_Icon = Icon_CharacterDevelopment_1;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.PositiveEnergy;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var ImmunityToNegativeFeat = Helpers.CreateBlueprint<BlueprintFeature>("ImmunityToNegativeFeat", bp => {
                bp.SetName("Negative energy Immunity");
                bp.SetDescription("You gain immunity to Negative energy.");
                bp.m_Icon = Icon_CharacterDevelopment_2;
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = DamageEnergyType.NegativeEnergy;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            // You are the exception
            var CharacterDevelopmentSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CharacterDevelopmentSelection", bp => {
                bp.SetName("Character Development Feat");
                bp.SetDescription("At 1st level, and every three levels thereafter, you can select one character development feat.");
                bp.m_DescriptionShort = Helpers.CreateString("PlotArmor.DescriptionShort", "Isekai Protagonists gain character development feats. These feats include immunity to {g|Encyclopedia:Critical}critical hits{/g}, {g|Encyclopedia:Energy_Damage}energy damage{/g}, or conditions.");
                bp.m_Icon = Icon_CharacterDevelopment_1;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    ImmunityToCriticalHitsFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToCriticalSneakFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToAbilityScoreDamageFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToEnergyDrainFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToAcidFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToColdFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToFireFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToElectricityFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToSonicFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToHolyFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToUnholyFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToPositiveFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToNegativeFeat.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    ImmunityToCriticalHitsFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToCriticalSneakFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToAbilityScoreDamageFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToEnergyDrainFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToAcidFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToColdFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToFireFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToElectricityFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToSonicFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToHolyFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToUnholyFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToPositiveFeat.ToReference<BlueprintFeatureReference>(),
                    ImmunityToNegativeFeat.ToReference<BlueprintFeatureReference>()
                };
            });

            //// Class Signature Features
            // Bonus Feats
            var IsekaiProtagonistBonusFeat = Helpers.CreateBlueprint<BlueprintFeature>("IsekaiProtagonistBonusFeat", bp => {
                bp.SetName("Bonus Feat");
                bp.SetDescription("Isekai Protagonists gain twice as many {g|Encyclopedia:Feat}feats{/g} as the other classes.");
                bp.m_DescriptionShort = Helpers.CreateString("IsekaiProtagonistBonusFeat.DescriptionShort", "Isekai Protagonists gain twice as many {g|Encyclopedia:Feat}feats{/g} as the other classes.");
                bp.m_Icon = BasicFeatSelection.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var IsekaiProtagonistSneakFeat = Helpers.CreateBlueprint<BlueprintFeature>("IsekaiProtagonistSneakFeat", bp => {
                bp.SetName("Sneak Attack");
                bp.SetDescription("Most characters gain advantages when they {g|Encyclopedia:Flanking}flank{/g} an enemy, {g|Encyclopedia:Attack}attack{/g} an enemy who can't see them or enjoy a similar fortunate position. Isekai Protagonists deal a tremendous amount of additional {g|Encyclopedia:Damage}damage{/g} in such a situation.");
                bp.m_DescriptionShort = Helpers.CreateString("IsekaiProtagonistSneakFeat.DescriptionShort", "Most characters gain advantages when they {g|Encyclopedia:Flanking}flank{/g} an enemy, {g|Encyclopedia:Attack}attack{/g} an enemy who can't see them or enjoy a similar fortunate position. Isekai Protagonists deal a tremendous amount of additional {g|Encyclopedia:Damage}damage{/g} in such a situation.");
                bp.m_Icon = SneakAttack.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            // Signature Abilities
            IsekaiProtagonistClass.m_SignatureAbilities = new BlueprintFeatureReference[4] {
                    IsekaiProtagonistBonusFeat.ToReference<BlueprintFeatureReference>(),
                    IsekaiProtagonistSneakFeat.ToReference<BlueprintFeatureReference>(),
                    PlotArmor.ToReference<BlueprintFeatureReference>(),
                    CharacterDevelopmentSelection.ToReference<BlueprintFeatureReference>()
            };
            IsekaiProtagonistProgression.LevelEntries = new LevelEntry[20] {
                Helpers.LevelEntry(1, IsekaiProtagonistProficiencies, IsekaiProtagonistCantripsFeature, IsekaiProtagonistBonusFeatSelection, SneakAttack, CharacterDevelopmentSelection, HaremMagnetFeature),
                Helpers.LevelEntry(2, IsekaiProtagonistBonusFeatSelection, UncannyDodge),
                Helpers.LevelEntry(3, SneakAttack, Evasion),
                Helpers.LevelEntry(4, IsekaiProtagonistBonusFeatSelection, CharacterDevelopmentSelection),
                Helpers.LevelEntry(5, SneakAttack, ImprovedUncannyDodge),
                Helpers.LevelEntry(6, IsekaiProtagonistBonusFeatSelection),
                Helpers.LevelEntry(7, SneakAttack, CharacterDevelopmentSelection),
                Helpers.LevelEntry(8, IsekaiProtagonistBonusFeatSelection),
                Helpers.LevelEntry(9, SneakAttack, ImprovedEvasion),
                Helpers.LevelEntry(10, IsekaiProtagonistBonusFeatSelection, CharacterDevelopmentSelection),
                Helpers.LevelEntry(11, SneakAttack),
                Helpers.LevelEntry(12, IsekaiProtagonistBonusFeatSelection),
                Helpers.LevelEntry(13, SneakAttack, CharacterDevelopmentSelection),
                Helpers.LevelEntry(14, IsekaiProtagonistBonusFeatSelection),
                Helpers.LevelEntry(15, SneakAttack),
                Helpers.LevelEntry(16, IsekaiProtagonistBonusFeatSelection, CharacterDevelopmentSelection),
                Helpers.LevelEntry(17, SneakAttack, PlotArmor, OtherworldlyStamina),
                Helpers.LevelEntry(18, IsekaiProtagonistBonusFeatSelection),
                Helpers.LevelEntry(19, SneakAttack, CharacterDevelopmentSelection),
                Helpers.LevelEntry(20, IsekaiProtagonistBonusFeatSelection, TrueMainCharacter)
            };
            IsekaiProtagonistProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(PlotArmor, HaremMagnetFeature, TrueMainCharacter),
                Helpers.CreateUIGroup(UncannyDodge, ImprovedUncannyDodge),
                Helpers.CreateUIGroup(Evasion, ImprovedEvasion)
            };
            IsekaiProtagonistProgression.m_UIDeterminatorsGroup = new BlueprintFeatureBaseReference[] {
                IsekaiProtagonistProficiencies.ToReference<BlueprintFeatureBaseReference>(),
                IsekaiProtagonistCantripsFeature.ToReference<BlueprintFeatureBaseReference>()
            };
            Helpers.RegisterClass(IsekaiProtagonistClass);
        }
    }
}