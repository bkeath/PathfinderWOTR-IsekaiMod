﻿using IsekaiMod.Content.Classes.IsekaiProtagonist;
using IsekaiMod.Content.Features.IsekaiProtagonist.Archetypes.Villain;
using IsekaiMod.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using TabletopTweaks.Core.Utilities;
using Kingmaker.Blueprints.Classes.Prerequisites;
using IsekaiMod.Content.Features.IsekaiProtagonist.Archetypes.EdgeLord;
using IsekaiMod.Content.Features.IsekaiProtagonist.Archetypes.GodEmperor;
using IsekaiMod.Content.Features.IsekaiProtagonist.Archetypes.Hero;
using static IsekaiMod.Main;
using Kingmaker.Blueprints.Classes.Selection;

namespace IsekaiMod.Content.Features.IsekaiProtagonist.InheritedClassFeature {
    internal class MagusDancerLegacy {
        private static string BaseArchetypeId = "1125145639129cf45b6b9b674cbd62b1";
        private static BlueprintArchetype BaseArchetype = BlueprintTools.GetBlueprint<BlueprintArchetype>(BaseArchetypeId);

        private static BlueprintProgression prog;

        public static void Configure() {
            prog = Helpers.CreateBlueprint<BlueprintProgression>(IsekaiContext, "MagusDancerLegacy", bp => {
                bp.SetName(IsekaiContext, "Magus Legacy - Spell Dancer");
                bp.SetDescription(IsekaiContext,
                    "So you were not really into martial arts or physical fighting of any kind. \n" +
                    "More like hundrets of hours of ballet lessons now serve as he basis of some really good dodging skills. \n" +
                    "Now with magical powers running through your veins you are even more flexible and thus have an even easier time dodging. \n" +
                    "According to some elf you met this is actually a special elven form of magic called spell dancing."
                    );
                bp.GiveFeaturesForPreviousLevels = true;
            });

            LegacySelection.RegisterForFeat(prog);
            //LegacySelection.Register(prog);
            //EdgeLordLegacySelection.Register(prog);
            //GodEmperorLegacySelection.Prohibit(prog);
            HeroLegacySelection.Register(prog);
            //VillainLegacySelection.Prohibit(prog);
        }
        public static void PatchProgression() {
            if (prog != null) {
                if (BaseArchetype == null) {
                    BaseArchetype = BlueprintTools.GetBlueprint<BlueprintArchetype>(BaseArchetypeId);
                    if (BaseArchetype == null) { return; }
                }
                LevelEntry[] addentries = new LevelEntry[] { };
                LevelEntry[] removeentries = new LevelEntry[] { };

                removeentries = removeentries.AppendToArray(BaseArchetype.RemoveFeatures);
                addentries = addentries.AppendToArray(BaseArchetype.AddFeatures);

                prog = StaticReferences.PatchClassProgressionBasedOnSeparateLists(prog, ClassTools.Classes.MagusClass, addentries, removeentries);

                BlueprintCharacterClassReference myClass = IsekaiProtagonistClass.GetReference();
                foreach (var level in BaseArchetype.AddFeatures) {
                    foreach (var candidate in level.Features) {
                        if (candidate != null && candidate is BlueprintFeatureSelection selection) {
                            StaticReferences.PatchClassIntoFeatureOfReferenceClass(selection, myClass, ClassTools.ClassReferences.MagusClass, 0, new BlueprintFeatureBase[] { });
                        } else {
                            if (candidate != null && candidate is BlueprintFeature feature) {
                                StaticReferences.PatchClassIntoFeatureOfReferenceClass(feature, myClass, ClassTools.ClassReferences.MagusClass, 0, new BlueprintFeatureBase[] { });
                            }
                        }
                    }
                }

                prog.AddPrerequisite<PrerequisiteNoFeature>(c => { c.m_Feature = MagusBasicLegacy.Get().ToReference<BlueprintFeatureReference>(); });
                prog.AddPrerequisite<PrerequisiteNoFeature>(c => { c.m_Feature = MagusArcherLegacy.Get().ToReference<BlueprintFeatureReference>(); });
                prog.AddPrerequisite<PrerequisiteNoFeature>(c => { c.m_Feature = MagusSpellbladeLegacy.Get().ToReference<BlueprintFeatureReference>(); });
            }
        }
        public static BlueprintProgression Get() {
            if (prog != null) return prog;
            return BlueprintTools.GetModBlueprint<BlueprintProgression>(IsekaiContext, "MagusDancerLegacy");
        }
    }
}
