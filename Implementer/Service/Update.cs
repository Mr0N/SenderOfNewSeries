using Implementer;
using Message.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace Message.Service
{
    internal class UpdateTelegram:BaseFactory<UpdateTelegram>
    {
        public async Task Start()
        {
            var result = context.infos.Where(a => a.CheckIsSendToTelegram == false);
            foreach (var item in result)
            {
                using var stream = await this.client.GetStreamAsync(item.UriImagePrevies);
                var file = new InputOnlineFile(stream, "file.webp");
                await botClient
                    .SendPhotoAsync(new Telegram.Bot.Types.ChatId(config.ChatUsername), 
                    file,@$"{item.NameSerial?.Trim(' ')}{Environment.NewLine}{item.SezonAndSeria?.Trim(' ')}");
                item.CheckIsSendToTelegram = true;
                await Task.Delay(3000);
            }
            await this.context.SaveChangesAsync();
        }
        TelegramBotClient botClient;
        Context context;
        Config config;
        HttpClient client;

        public UpdateTelegram(TelegramBotClient botClient,Context context,Config config,HttpClient client)
        {
            this.botClient = botClient;
            this.context = context;
            this.config = config;
            this.client = client;
        }
    }
}
