using HarmonyLib;
using NeosModLoader;
using FrooxEngine;

namespace ModNameGoesHere
{
    public class ModNameGoesHere : NeosMod
    {
        public override string Name => "ModNameGoesHere";
        public override string Author => "AuthorNameGoesHere";
        public override string Version => "modVersionNumber";
        public override string Link => "https://github.com/GithubUsername/ModNameGoesHere/";
        private static ModConfiguration Config;
	
	    [AutoRegisterConfigKey] private static readonly ModConfigurationKey<bool> Enabled = new ModConfigurationKey<bool>("Enabled", "Enable/Disable the Mod", () => true);
	
        public override void OnEngineInit()
        {
	        Config = GetConfiguration();
            Config.Save(true);
            Harmony harmony = new Harmony("net.AuthorNameGoesHere.ModNameGoesHere");
            harmony.PatchAll();
        }
	
		// https://harmony.pardeike.net/articles/patching-prefix.html
        [HarmonyPatch(typeof(FrooxEngine.LogixTip), "name of method you want to patch")]
        class ModNameGoesHere_Prefix_Patch
        {
            static bool Prefix()
            {
                if (!Config.GetValue(Enabled)) { return true; }
                Warn("Code Here")
                return false;
            }
        }
		
        // https://harmony.pardeike.net/articles/patching-postfix.html
        [HarmonyPatch(typeof(FrooxEngine.LogixTip), "name of method you want to patch")]
        class ModNameGoesHere_Postfix_Patch
        {
            static void Postfix()
            {
                if (Config.GetValue(Enabled) )
                {
                    Warn("Code Here")
                }
            }
        }

        // https://harmony.pardeike.net/articles/patching-transpiler.html
        [HarmonyPatch(typeof(FrooxEngine.LogixTip), "name of method you want to patch")]
        public class ModNameGoesHere_Transpiler_Patch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var code = new List<CodeInstruction>(instructions);
                Warn("Code Here")
                return code;
            }  
        }
    }
}
