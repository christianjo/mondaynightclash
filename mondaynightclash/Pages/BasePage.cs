using System;
using Xamarin.Forms;
using System.ComponentModel;

namespace mondaynightclash
{
	public class BasePage : ContentPage
	{

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var basePageModel = this.BindingContext as FreshMvvm.FreshBasePageModel;
			if (basePageModel != null)
			{
				if (basePageModel.IsModalAndHasPreviousNavigationStack())
				{
					//Todo: Endre dette.
					if (ToolbarItems.Count < 1)
					{
						var closeModal = new ToolbarItem("Close Modal", "", () =>
						{
							basePageModel.CoreMethods.PopPageModel(true,true);
						});

						ToolbarItems.Add(closeModal);
					}
				}
			}
		}
	}
}

