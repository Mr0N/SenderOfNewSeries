using Implementer;
using Message.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Service
{
    internal class Extract:BaseFactory<Extract>
    {
        public async Task Start()
        {
            
            foreach (var item in await this.parser.Get())
            {
                if (context.infos
                    .Where(a => a.UriViews == item.UriViews)
                    .Count() == 0) {
                    await context.infos.AddAsync(new Info()
                    {
                        CheckIsSendToTelegram = false,
                        NameSerial = item.NameSerial,
                        SezonAndSeria = item.SezonAndSeria,
                        UriImagePrevies = item.UriImagePrevies,
                        UriViews = item.UriViews
                    });
                }
            }
            context.SaveChanges();
        }
        Parser parser;
        Context context;
        public Extract(Context context,Parser parser)
        {
            this.parser = parser;
            this.context = context;
        }
    }
}
