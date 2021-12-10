﻿using Autofac;
using Cryptollet.Common.Database;
using Cryptollet.Common.Database.Migrations;
using Cryptollet.Common.Extensions;
using Cryptollet.Common.Models;
using Cryptollet.Modules.Loading;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Reflection;
using Xamarin.Forms;

namespace Cryptollet
{
    public partial class App : Application
    {
        public static IContainer Container;
        public App()
        {
            InitializeComponent();
            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .AsImplementedInterfaces()
                   .AsSelf();

            builder.RegisterType<Repository<User>>().As<IRepository<User>>();
            builder.RegisterType<Repository<Transaction>>().As<IRepository<Transaction>>();

            //get container
            Container = builder.Build();

            //run database migrations
            var migrationService = Container.Resolve<IMigrationService>();
            migrationService.RunDatabaseMigrations().SafeFireAndForget(false);

            //set first page
            MainPage = Container.Resolve<LoadingView>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            AppCenter.Start("ios=6260e35e-b54b-42f0-afbe-67733d0ea0f0;" +
                  "android=2ae20fb5-2428-434b-8469-bc9b319ef7b6",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}
