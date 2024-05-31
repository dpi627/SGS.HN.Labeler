﻿using Microsoft.Extensions.DependencyInjection;
using SGS.HN.Labeler.Service.Implement;
using SGS.HN.Labeler.Service.Interface;

namespace SGS.HN.Labeler.Extension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 註冊 Service
        /// </summary>
        /// <param name="services">服務集合</param>
        /// <returns>服務集合</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IExcelConfigService, ExcelConfigService>();
            return services;
        }

        /// <summary>
        /// 註冊 Repository
        /// </summary>
        /// <param name="services">服務集合</param>
        /// <returns>服務集合</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<ITestRecordRepository, TestRecordRepository>();
            return services;
        }

        /// <summary>
        /// 註冊其他服務
        /// </summary>
        /// <param name="services">服務集合</param>
        /// <returns>服務集合</returns>
        public static IServiceCollection AddMiscs(this IServiceCollection services)
        {
            services.AddSingleton<IServiceProvider>(services.BuildServiceProvider());
            return services;
        }

        public static IServiceCollection AddForms(this IServiceCollection services)
        {
            services.AddSingleton<frmMain>();
            return services;
        }

        /// <summary>
        /// 取得或建立服務
        /// </summary>
        /// <typeparam name="T">服務類型</typeparam>
        /// <param name="serviceProvider">服務提供者</param>
        /// <returns>服務實例</returns>
        public static T GetOrCreateService<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(serviceProvider);
        }
    }
}