using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Projectiles
{
    class DimWarp2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Name = "Dimensional Warp";
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 10;
            Projectile.ai[0] = 0;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss && !target.townNPC)
            {
                Projectile warppoint = Main.player[Projectile.owner].GetModPlayer<ConfectionPlayer>().DimensionalWarp;
                target.position.X = warppoint.position.X;
                target.position.Y = warppoint.position.Y;
                target.HealEffect(1);
                Main.player[Projectile.owner].GetModPlayer<ConfectionPlayer>().DimensionalWarp.Kill();
                Main.player[Projectile.owner].GetModPlayer<ConfectionPlayer>().DimensionalWarpIndex = -1;
                Projectile.ai[0] = 1;
                Projectile.Kill();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.player[Main.myPlayer].whoAmI == target.whoAmI)
            {
                Projectile warppoint = Main.player[Projectile.owner].GetModPlayer<ConfectionPlayer>().DimensionalWarp;
                target.Teleport(warppoint.position, 1);
                target.HealEffect(1);
                if (target.HasBuff(ModContent.BuffType<Buffs.GoneBananas>()))
                {
                    target.Hurt(PlayerDeathReason.ByCustomReason("DimensionSplit"), (int)((target.statLifeMax2 + target.statDefense) * (target.endurance + 1) / 7), 0);
                }
                target.AddBuff(ModContent.BuffType<Buffs.GoneBananas>(), 360);
                Projectile.ai[0] = 1;
                Projectile.Kill();
            }
        }
        public override bool PreKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Player owner = Main.player[Main.myPlayer];
                if (Projectile.ai[0] == 0)
                {
                    owner.Teleport(owner.GetModPlayer<ConfectionPlayer>().DimensionalWarp.position, 1);
                    owner.GetModPlayer<ConfectionPlayer>().DimensionalWarp.Kill();
                    if (owner.HasBuff(ModContent.BuffType<Buffs.GoneBananas>()))
                    {
                        owner.Hurt(PlayerDeathReason.ByCustomReason("DimensionSplit"), (int)((owner.statLifeMax2 + owner.statDefense) * (owner.endurance + 1) / 7), 0);
                    }
                    owner.AddBuff(ModContent.BuffType<Buffs.GoneBananas>(), 360);
                }
                owner.GetModPlayer<ConfectionPlayer>().DimensionalWarpIndex = -1;
            }
            return true;
        }
    }
}
