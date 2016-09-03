﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using MALClient.Adapters;
using MALClient.Adapters.Credentails;

namespace MALClient.XShared.ViewModels
{
    public static class ResourceLocator
    {
        public static IApplicationDataService ApplicationDataService
            => SimpleIoc.Default.GetInstance<IApplicationDataService>();

        public static IPasswordVault PasswordVaultProvider => SimpleIoc.Default.GetInstance<IPasswordVault>();

        public static IDataCache DataCacheService => SimpleIoc.Default.GetInstance<IDataCache>();

        public static IMessageDialogProvider MessageDialogProvider => SimpleIoc.Default.GetInstance<IMessageDialogProvider>();

        public static IClipboardProvider ClipboardProvider => SimpleIoc.Default.GetInstance<IClipboardProvider>();

        public static ISystemControlsLauncherService SystemControlsLauncherService  => SimpleIoc.Default.GetInstance<ISystemControlsLauncherService>();

        public static ILiveTilesManager LiveTilesManager  => SimpleIoc.Default.GetInstance<ILiveTilesManager>();

        public static IImageDownloaderService ImageDownloaderService  => SimpleIoc.Default.GetInstance<IImageDownloaderService>();
    }
}