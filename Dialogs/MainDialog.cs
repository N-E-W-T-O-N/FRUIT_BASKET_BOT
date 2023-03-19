// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.18.1

using AdaptiveExpressions;
using FruitBasket.CognitiveModels;
using FruitBasket.Cards;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasket.Dialogs
{

    public class MainDialog : ComponentDialog
    {
        protected readonly   IStatePropertyAccessor<Fruit> _userProfileAccessor;
        //private readonly Fruit _luisRecognizer;
        private readonly ILogger _logger;

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(UserState userState, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            _userProfileAccessor = userState.CreateProperty<Fruit>("UserProfile");
            // _luisRecognizer = luisRecognizer;
            _logger = logger;

            var waterfallSteps = new WaterfallStep[]
            {
                SelectFruitAsync,  //select fruit
                SelectAmount, //Select amount
                ConfirmOrderAsync,
                FinalResultAsyn,
            };

            
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), AgePromptValidatorAsync));
 
            InitialDialogId = nameof(WaterfallDialog);
        }

        // Shows a warning if the requested From or To cities are recognized as entities but they are not in the Airport entity list.
        // In some cases LUIS will recognize the From and To composite entities as a valid cities but the From and To Airport values
        // will be empty if those entity values can't be mapped to a canonical item in the Airport.
        private static async Task<DialogTurnResult> SelectFruitAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please Choice the Fruit you want to get delievered"),
                    Choices = ChoiceFactory.ToChoices(Fruit.inventory),
                }, cancellationToken);

        }

        private static async Task<DialogTurnResult> SelectAmount(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["selected"] = ((FoundChoice)stepContext.Result).Value;
 
                // User said "yes" so we will be prompting for the age.
                // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
                 

                return await stepContext.PromptAsync(nameof(NumberPrompt<int>),
                    new PromptOptions {
                        Prompt = MessageFactory.Text("Enter the Amount you need."),
                        RetryPrompt = MessageFactory.Text("The value entered must be greater than 0 and less than 150."),
                    },
                    cancellationToken);
        }
        private static Task<bool> AgePromptValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // This condition is our validation rule. You can also change the value at this point.
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0 && promptContext.Recognized.Value < 50);
        }
        private static async Task<DialogTurnResult> ConfirmOrderAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["amount"] = (int) stepContext.Result ;

            return await stepContext.PromptAsync(nameof(ConfirmPrompt) ,
                new PromptOptions{
                Prompt = MessageFactory.Text("You are sure about this")
                },
              cancellationToken  );
        }
        private static async Task<DialogTurnResult> FinalResultAsyn(WaterfallStepContext stepContext, CancellationToken cancellation)
        {

             

            if ((bool)stepContext.Result)
            {
                var fruit = (string)stepContext.Values["selected"];
                var amount = (int)stepContext.Values["amount"] ;
                var msg = $"You have Chosen {fruit} of amount {amount} KG";

                var r = Card.GiveAdaptiveCard(fruit, msg, amount);

                 //await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellation);
                  await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(r));


           }
            else {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thanks. Your profile will not be kept."), cancellation);
            }
            //sConsole.WriteLine(stepContext.Context.Activity.ChannelData?.ToString());
            return await stepContext.EndDialogAsync();//cancellationToken:c);
        }


    }
}
