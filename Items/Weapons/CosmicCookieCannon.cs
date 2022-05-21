using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheConfectionRebirth.Items.Weapons
{
    public class CosmicCookieCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Cookie Cannon");
            Tooltip.SetDefault("'But wait? Doesn't it shoot cosmic brownies instead?'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 20;
            Item.value = Item.sellPrice(silver: 400);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item9;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.CosmicCookie>();
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.FallenStar;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeable.NeapoliniteBar>(), 12).AddIngredient(ItemID.StarCannon, 1).AddTile(TileID.MythrilAnvil).Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}