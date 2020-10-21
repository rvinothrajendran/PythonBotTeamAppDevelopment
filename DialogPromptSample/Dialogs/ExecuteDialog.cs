using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace AdaptiveCardPromptSample.Dialogs
{
    public class ExecuteDialog : ComponentDialog
    {
        public ExecuteDialog() : base(nameof(ExecuteDialog))
        {
            AddDialog(new MainDialog());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                CallPromptDlg,
                Result,
            }));
        }

        private async Task<DialogTurnResult> Result(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            return await stepcontext.EndDialogAsync(null, cancellationtoken);
        }

        private async Task<DialogTurnResult> CallPromptDlg(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            return await stepcontext.BeginDialogAsync(nameof(MainDialog), null, cancellationtoken);
        }
    }
}
