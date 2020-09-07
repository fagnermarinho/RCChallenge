using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RCC.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RCC.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILikesUpdater _likeUpdater;
        private readonly int _timeDelay;

        public Worker(IConfiguration configuration, ILikesUpdater likeUpdater)
        {
            _likeUpdater = likeUpdater;

            _timeDelay = Convert.ToInt32(configuration.GetSection("RCC:TimeDelayUpdateLikes").Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _likeUpdater.Execute();

                await Task.Delay(_timeDelay, stoppingToken);
            }
        }
    }
}
