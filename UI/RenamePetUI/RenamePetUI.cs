﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.UI;

namespace PetRenamer.UI.RenamePetUI
{
	//ty jopojelly and darthmorf
	internal class RenamePetUI : UIPanel
	{
		private UIText titleText;
		private UIBetterTextBox commandInput;
		private UIPanel applyButton;
		private UIPanel clearButton;
		private VanillaItemSlotWrapper itemSlot;

		private readonly static Color bgColor = new Color(73, 94, 171);
		private readonly static Color hoverColor = new Color(100, 118, 184);
		private readonly static string sampleName = "Sample Name";

		internal const int width = 480;
		internal const int height = 155;

		//UI aligned with center right below the player
		internal int RelativeLeft => Main.screenWidth / 2 - width / 2;
		internal int RelativeTop => Main.screenHeight / 2 + 42; //Half the player height on 200% zoom

		internal bool firstDraw = true;

		//Used to notify the UI to save the item instead of dropping it
		internal static bool saveItemInUI = false;

		public override void OnInitialize()
		{
			Width.Pixels = width;
			Height.Pixels = height;
			Top.Pixels = int.MaxValue / 2;
			Left.Pixels = int.MaxValue / 2;

			float nextElementY = -PaddingTop / 2;

			titleText = new UIText("Rename Pet")
			{
				Top = { Pixels = nextElementY },
				HAlign = 0.5f
			};
			Append(titleText);

			nextElementY += 20;
			string name = sampleName;

			itemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f)
			{
				Left = { Pixels = 0f },
				Top = { Pixels = nextElementY },
				HAlign = 0.5f,
				ValidItemFunc = item => item.IsAir || PetRenamer.IsPetItem(item)
			};
			itemSlot.OnEmptyMouseover += () => { Main.hoverItemName = "Place a pet summoning item here"; };
			Append(itemSlot);

			nextElementY += 49 + 6; //49 is the size of an item slot, 6 is for visual gap

			Item uiItem = Main.mouseItem;
			bool skipCheck = false;

			Item renamePetUIItem = Main.LocalPlayer.GetModPlayer<PRPlayer>().renamePetUIItem;
			if (!renamePetUIItem.IsAir)
			{
				skipCheck = true;
				uiItem = renamePetUIItem;
			}

			if (skipCheck || !uiItem.IsAir && itemSlot.Valid(uiItem))
			{
				if (uiItem.type != ModContent.ItemType<MysteryItem>())
				{
					PRItem petItem = uiItem.GetGlobalItem<PRItem>();
					name = petItem.petName;
				}
				itemSlot.Item = uiItem.Clone();
				uiItem.TurnToAir(); //The previous item reference (mouse item or saved item) gets cleared
			}

			commandInput = new UIBetterTextBox("Enter Name Here", name)
			{
				BackgroundColor = Color.White,
				Top = { Pixels = nextElementY },
				Width = { Precent = 1f },
				Height = { Pixels = 30f }
			};
			Append(commandInput);

			nextElementY += 36;

			float ratioFromCenter = 0.1f;

			applyButton = new UIPanel()
			{
				Top = { Pixels = nextElementY },
				Width = { Pixels = 60f },
				Height = { Pixels = 30f },
				HAlign = 0.5f - ratioFromCenter,
				BackgroundColor = bgColor
			};
			applyButton.OnClick += (evt, element) => { ApplyNameToItem(); };

			UIText applyButonText = new UIText("Apply")
			{
				Top = { Pixels = -4f },
				Left = { Pixels = -2f }
			};
			applyButton.Append(applyButonText);
			Append(applyButton);

			clearButton = new UIPanel()
			{
				Top = { Pixels = nextElementY },
				Width = { Pixels = 60f },
				Height = { Pixels = 30f },
				HAlign = 0.5f + ratioFromCenter,
				BackgroundColor = bgColor
			};
			clearButton.OnClick += (evt, element) => { ClearTextField(); };

			UIText clearButtonText = new UIText("Clear")
			{
				Top = { Pixels = -4f },
				Left = { Pixels = -2f }
			};
			clearButton.Append(clearButtonText);
			Append(clearButton);

			UIQuitButton quitButton = new UIQuitButton("Close");
			//Using initializer pattern here didn't work for some reason with the Width
			quitButton.Top.Pixels = -PaddingTop / 2;
			quitButton.Left.Pixels = width - PaddingRight - quitButton.Width.Pixels - 8;
			quitButton.OnClick += (evt, element) => { PetRenamer.CloseRenamePetUI(); };
			Append(quitButton);
		}

		//Called when the state is set
		public override void OnActivate()
		{
			base.OnActivate();
			Main.PlaySound(SoundID.MenuOpen);
		}

		//Called when this.Deactivate() is called, aka when SetState is called with null
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			if (!saveItemInUI && !Main.gameMenu)
			{
				Main.PlaySound(SoundID.MenuClose);
			}

			Item item = itemSlot.Item;
			if (saveItemInUI)
			{
				saveItemInUI = false;
				if (!item.IsAir)
				{
					Main.LocalPlayer.GetModPlayer<PRPlayer>().renamePetUIItem = item.Clone();
					//Save item for next time the player uses the UI
				}
			}
			else
			{
				//Give item back to player
				if (!item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnClonedItem(item, item.stack);
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			//Constantly lock the UI in the position regardless of resolution changes
			Left.Pixels = RelativeLeft;
			Top.Pixels = RelativeTop;

			applyButton.BackgroundColor = applyButton.IsMouseHovering ? hoverColor : bgColor;
			clearButton.BackgroundColor = clearButton.IsMouseHovering ? hoverColor : bgColor;
		}

		private void ApplyNameToItem()
		{
			Item item = itemSlot.Item;
			if (!item.IsAir && itemSlot.Valid(item))
			{
				PRItem petItem = item.GetGlobalItem<PRItem>();
				petItem.petName = commandInput.currentString;
				petItem.petOwner = Main.LocalPlayer.name;
				Main.PlaySound(SoundID.MenuTick);
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (firstDraw)
			{
				firstDraw = false;
				return;
			}

			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.showItemIcon = false;
				Main.ItemIconCacheUpdate(0);
			}
			base.DrawSelf(spriteBatch);
		}

		private void ClearTextField()
		{
			if (commandInput.currentString.Length > 0)
			{
				Main.PlaySound(SoundID.MenuTick);
			}
			commandInput.SetText("");
		}
	}
}