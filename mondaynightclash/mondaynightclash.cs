using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using FreshMvvm;
using Xamarin.Forms;

namespace mondaynightclash
{
	public class App : Application
	{

		public App()
		{
			SetupIOC();

			var selectPlayersListPageModel = FreshPageModelResolver.ResolvePageModel<SelectPlayersListPageModel>();
			var navContainer = new FreshNavigationContainer(selectPlayersListPageModel, NavigationStacks.MainAppStack);
			//MainPage = new NavigationPage(new LaunchPage(this));

			MainPage = navContainer;
		}

		void SetupIOC()
		{

			FreshIOC.Container.Register<IDataService, DataService>();
			FreshIOC.Container.Register(UserDialogs.Instance);
			//FreshIOC.Container.Register<IFreshNavigationService>(new CustomImplementedNav(), NavigationStacks.CustomImplementedNav);

			FreshIOC.Container.Register<IFreshNavigationService>(LoadBasicNav(), NavigationStacks.GameNavigationStackBasic);
			//Todo: Dette instansierer opp TabbedNav, noe som er for tidlig.
			//FreshIOC.Container.Register<IFreshNavigationService>(LoadTabbedNav(), NavigationStacks.GameNavigationStackTabbed);

		}

		public FreshNavigationContainer LoadBasicNav()
		{
			var page = FreshPageModelResolver.ResolvePageModel<TablePageModel>();
			var basicNavContainer = new FreshNavigationContainer(page);
			return basicNavContainer;
		}

		//public FreshTabbedNavigationContainer LoadTabbedNav()
		//{
		//	//Todo: Dette instansiereres opp for tidlig
		//	var tabbedNavigation = new FreshTabbedNavigationContainer();
		//	tabbedNavigation.AddTab<TablePageModel>("Tabell", null, null);
		//	tabbedNavigation.AddTab<TeamsListPageModel>("Teams", null, null);
		//	return tabbedNavigation;
		//}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}

}
