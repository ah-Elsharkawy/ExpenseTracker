using Abp.Dependency;
using Abp.Domain.Repositories;
using ExpenseTracker.Models;
using System;

namespace ExpenseTracker.Scedulers
{
    public class Activator : Hangfire.JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public Activator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }



    }
}
