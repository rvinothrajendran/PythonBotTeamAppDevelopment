using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bot.Builder.Community.Dialogs.Prompts;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdaptiveCardPromptSample.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        public MainDialog() : base(nameof(MainDialog))
        {
            //Added AdaptiveCard Prompt
            AddDialog(new AdaptiveCardPrompt(nameof(AdaptiveCardPrompt), CreateAdaptiveCardPromptSettings()));

            //Added NumberWithUnitType Prompt
            AddDialog(new NumberWithUnitPrompt("Currency", NumberWithUnitPromptType.Currency));
            AddDialog(new NumberWithUnitPrompt("Age", NumberWithUnitPromptType.Age));
            AddDialog(new NumberWithUnitPrompt("Dimension", NumberWithUnitPromptType.Dimension));
            AddDialog(new NumberWithUnitPrompt("Temperature", NumberWithUnitPromptType.Temperature));


            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                DisplayAdaptiveCard,
                DisplayPromptType,
                PromptResult
            }));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> PromptResult(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            if (stepcontext.Result != null)
            {
                var result = string.Empty;

                var resultType = stepcontext.Result.GetType();

                if (resultType.Name == "NumberWithUnitResult")
                {
                    var unitResult =  (NumberWithUnitResult) stepcontext.Result;
                    result = $@"Bot received Value : {unitResult.Value} and Unit {unitResult.Unit}";
                }

                await stepcontext.Context.SendActivityAsync(MessageFactory.Text(result), cancellationtoken);
            }

            await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);
            return await stepcontext.BeginDialogAsync(nameof(WaterfallDialog), cancellationToken: cancellationtoken);
        }

        private static async Task<DialogTurnResult> DisplayPromptType(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            if (!(stepcontext.Result is AdaptiveCardPromptResult adaptiveResult))
                return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);

            if (!(adaptiveResult.Data is JObject result))
                return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);

            var resultArray = result.Properties().Select(p => $"{p.Value}").ToList();
            if (!(resultArray.Count > 0))
                return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);

            var promptOptions = resultArray[0] switch
            {
                "Currency" => new PromptOptions
                {
                    Prompt = new Activity {Type = ActivityTypes.Message, Text = "Enter a currency."}
                },
                "Temperature" => new PromptOptions()
                {
                    Prompt = new Activity {Type = ActivityTypes.Message, Text = "Enter a Temperature."}
                },
                _ => null
            };

            if (!string.IsNullOrEmpty(resultArray[0]) && promptOptions != null)
            {
                return await stepcontext.PromptAsync(resultArray[0], promptOptions, cancellationtoken);
            }
            return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);
        }

        private async Task<DialogTurnResult> DisplayAdaptiveCard(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            return await stepcontext.PromptAsync(nameof(AdaptiveCardPrompt), new PromptOptions(), cancellationtoken);
        }

        private static AdaptiveCardPromptSettings CreateAdaptiveCardPromptSettings()
        {
            var cardJson = File.ReadAllText("promptTypes.json");

            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            var promptSettings = new AdaptiveCardPromptSettings
            {
                Card = cardAttachment
            };
            return promptSettings;
        }
    }
}
