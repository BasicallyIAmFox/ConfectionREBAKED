using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Weapons;

namespace TheConfectionRebirth.NPCs
{
    public class BigMimicConfection : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Confection Mimic");
            Main.npcFrameCount[NPC.type] = 14;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 40;
            NPC.damage = 180;
            NPC.defense = 34;
            NPC.lifeMax = 3500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 87;
            AIType = NPCID.BigMimicHallow;
            AnimationType = NPCID.BigMimicHallow;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionUndergroundBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                new FlavorTextBestiaryInfoElement("Mimics that are engolthed by the Confection turn into a Monsterest cake. A certain key can bring them to life.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneDirtLayerHeight /*&& spawnInfo.Player.ModBiome<ConfectionUndergroundBiome>()*/)
            {
                return 0.01f;
            }
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ModContent.ItemType<CookieCrumbler>(), ModContent.ItemType<SweetTooth>(), ModContent.ItemType<SweetHook>(), ModContent.ItemType<CreamSpray>()));
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 5, 10));
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterManaPotion, 1, 5, 15));
        }
    }
}