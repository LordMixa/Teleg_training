using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Teleg_training.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Serilog;

namespace Teleg_training
{
    internal class Program
    {
        static DBLogic? dBLogic;
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\Users\\mishy\\source\\repos\\Teleg_training\\Teleg_training\\logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information($"Starting up ");
            var botClient = new TelegramBotClient("6039744763:AAH2Z5I1jwSzkTXUTD2NglT38NSz06s7Tp8");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            var serviceProvider = new ServiceCollection()
            .AddDbContext<ProgramListContext>()
            .AddAutoMapper(typeof(UserMappingProfile))
            .BuildServiceProvider();
            dBLogic = new DBLogic();

            botClient.StartReceiving(
                updateHandler: Update,
                pollingErrorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            Console.ReadLine();

            cts.Cancel();
        }

        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message is not { } message)
            {
                if (update.CallbackQuery?.Data != null && update.CallbackQuery.Message != null)
                {
                    if (update.CallbackQuery?.Data == "male_menu")
                    {
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "Choose your level",
                        replyMarkup: GetButtonsListProgMaleLevel(),
                        cancellationToken: token);
                        await client.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: null
                        );
                        Log.Information($"User {update.CallbackQuery.From.FirstName} male_menu");
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        return;
                    }
                    else if (update.CallbackQuery?.Data == "female_menu")
                    {
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "Choose your level",
                        replyMarkup: GetButtonsListProgFemaleLevel(),
                        cancellationToken: token);
                        await client.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: null
                        );
                        Log.Information($"User {update.CallbackQuery.From.FirstName} female_menu");
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "back_to_main")
                    {
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "Menu",
                        replyMarkup: GetButtonsMain(),
                        cancellationToken: token);
                        await client.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: null
                        );
                        Log.Information($"User {update.CallbackQuery.From.FirstName} back_to_main");

                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "back_to_gender")
                    {
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "Choose your gender",
                        replyMarkup: GetButtonsListProgSex(),
                        cancellationToken: token);
                        await client.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: null
                        );
                        Log.Information($"User {update.CallbackQuery.From.FirstName} back_to_gender");

                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "male_start" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("male", "start");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} male_start");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "male_mid" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("male", "mid");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} male_mid");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "male_pro" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("male", "pro");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} male_pro");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "female_start" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("female", "start");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} female_start");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "female_mid" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("female", "mid");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} female_mid");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "female_pro" && dBLogic != null)
                    {
                        string progs = dBLogic.GetStringListOfPrograms("female", "pro");
                        Message sentMessage = await client.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: progs,
                        cancellationToken: token);
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                        Log.Information($"User {update.CallbackQuery.From.FirstName} female_pro");

                        return;
                    }
                    else if (update.CallbackQuery?.Data == "like" && update.CallbackQuery.Message.Text != null && dBLogic != null)
                    {
                        string[] lines = update.CallbackQuery.Message.Text.Split('\n');
                        string firstLine = lines[0];
                        ModelList model = dBLogic.GetProgram('\\' + firstLine);
                        string infolike = await dBLogic.LikeProgram(model, update.CallbackQuery.From.Id);
                        if (infolike == "like")
                            Log.Information($"User {update.CallbackQuery.From.FirstName} like {model.CodeName}");
                        else
                            Log.Information($"User {update.CallbackQuery.From.FirstName} unlike {model.CodeName}");
                        await client.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);

                        return;
                    }
                }
                return;
            }
            if (message.Text is not { } messageText)
                return;
            if (messageText.ToLower() == "/help" || messageText.ToLower() == "/start")
            {
                if (message != null && dBLogic != null && message.From != null && message.From.Username != null)
                {
                    if (!dBLogic.GetInfoUserExist(message.From.Id))
                    {
                        dBLogic.AddUser(message.From.Id, message.From.Username);
                        Log.Information($"New User {update.Message.Chat.Username} {message.From.Id} added on DB");
                    }
                    Message sentMessage = await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Help commands",
                    replyMarkup: GetButtonsMain(),
                    cancellationToken: token);
                    Log.Information($"User {update.Message.Chat.Username} help");

                    return;
                }
            }
            if (messageText.ToLower() == "info" && message != null && message.From != null)
            {
                Message sentMessage = await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "This bot was created by LordMixa for non-commercial use. This bot was created to help athletes find the necessary training programs and other information. To call the navigation menu: /help",
                replyMarkup: GetButtonsMain(),
                cancellationToken: token);
                Log.Information($"User {message.From.FirstName} info");

                return;
            }
            if (messageText.ToLower() == "list of programs" && message != null && message.From != null)
            {
                Message sentMessage = await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose your gender",
                replyMarkup: GetButtonsListProgSex(),
                cancellationToken: token);
                Log.Information($"User {message.From.FirstName} list of progs");
                return;
            }
            if (messageText.ToLower() == "top of programs" && message != null && message.From != null && dBLogic != null)
            {
                string progs = dBLogic.GetTop();
                Message sentMessage = await client.SendTextMessageAsync(
                        message.Chat.Id,
                        text: progs,
                        replyMarkup: GetButtonsMain(),
                        cancellationToken: token);
                Log.Information($"User {message.From.FirstName} top of programs");

                return;
            }
            if (messageText.ToLower() == "products" && message != null && message.From != null && dBLogic != null)
            {
                string prod = dBLogic.GetProducts();
                Message sentMessage = await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: prod,
                replyMarkup: GetButtonsMain(),
                cancellationToken: token);
                Log.Information($"User {message.From.FirstName} products");

                return;
            }
            if (messageText.ToLower() == "liked programs" && message != null && message.From != null && update != null && update.Message != null && update.Message.From != null && dBLogic != null)
            {
                string progs = dBLogic.GetLikedLists(update.Message.From.Id);

                Message sentMessage = await client.SendTextMessageAsync(
                        message.Chat.Id,
                        text: progs,
                        replyMarkup: GetButtonsMain(),
                        cancellationToken: token);
                Log.Information($"User {message.From.FirstName} liked programs");

                return;
            }
            else if (dBLogic != null)
            {

                ModelList model = dBLogic.GetProgram(messageText);
                if (model != null && message != null && message.From != null)
                {
                    if (dBLogic.GetInfoLikeProgram(model, message.From.Id) == "like")
                    {
                        Message sentMessage1 = await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: model.CodeName + '\n' + model.Program,
                        replyMarkup: GetButtonLike(),
                        cancellationToken: token);
                        Log.Information($"User {message.From.FirstName} get program {model.CodeName}");

                        return;
                    }
                    else
                    {
                        Message sentMessage2 = await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: model.CodeName + '\n' + model.Program,
                        replyMarkup: GetButtonUnLike(),
                        cancellationToken: token);
                        Log.Information($"User {message.From.FirstName} get program {model.CodeName}");

                        return;
                    }
                }
                else if (message != null && message.From != null)
                {
                    Message sentMessage3 = await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Unknown command",
                    replyMarkup: GetButtonsMain(),
                    cancellationToken: token);
                    Log.Information($"User {message.From.FirstName} unknown command");

                    return;
                }
            }
        }
        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Log.Error(errorMessage);

            if (exception is ApiRequestException apiException)
            {
                switch (apiException.ErrorCode)
                {
                    case 400:
                        Log.Warning("Ignoring 400 error");
                        break;
                    default:
                        Log.Warning("Unhandled API error");
                        break;
                }
            }

            return Task.CompletedTask;
        }
        private static IReplyMarkup GetButtonsMain()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "List of programs", "Top of programs" },
                new KeyboardButton[] { "Info", "Products", "Liked Programs"},
            })
            {
                ResizeKeyboard = true
            };
            replyKeyboardMarkup.OneTimeKeyboard = true;
            return replyKeyboardMarkup;
        }
        private static IReplyMarkup GetButtonsListProgSex()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Male", callbackData: "male_menu")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Female", callbackData: "female_menu"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Back", callbackData: "back_to_main"),
                },
            });
            return inlineKeyboard;
        }
        private static IReplyMarkup GetButtonsListProgMaleLevel()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Start", callbackData: "male_start")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Middle", callbackData: "male_mid"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Pro", callbackData: "male_pro"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Back", callbackData: "back_to_gender"),
                },
            });
            return inlineKeyboard;
        }
        private static IReplyMarkup GetButtonsListProgFemaleLevel()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
             {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Start", callbackData: "female_start")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Middle", callbackData: "female_mid"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Pro", callbackData: "female_pro"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Back", callbackData: "back_to_gender"),
                },
            });
            return inlineKeyboard;
        }
        private static IReplyMarkup GetButtonLike()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
             {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Like♡", callbackData: "like")
                },
            });
            return inlineKeyboard;
        }
        private static IReplyMarkup GetButtonUnLike()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
             {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Like♥", callbackData: "like")
                },
            });
            return inlineKeyboard;
        }
    }
}

