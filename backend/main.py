import utils.console
import utils.llm
import utils.mydiscord
import os
from dotenv import load_dotenv

load_dotenv()


TOKEN = os.getenv('DISCORD_TOKEN')

master_word = None
master_Norm = 0

words = []

def OutputMessage(norm):
    if norm > 1.0:
        return ("正解です！")
    elif norm > 0.9:
        return ("かなり近いです")
    elif norm > 0.7:
        return ("近いです")
    elif norm > 0.4:
        return "当たらずとも遠からずです"
    elif norm > 0.25:
        return "部分的に遠いです"
    else:
        return "遠いです"

async def OnInited():
    await utils.mydiscord.print_log("言語モデルを読み込んでいます...")
    await utils.llm.Load("model.vec")
    await utils.mydiscord.set_Status("準備完了")
    await utils.mydiscord.print_log("言語モデルを読み込みました")
    await utils.mydiscord.print_log("ゴールの単語を`GOAL=||単語||`の形式で設定してください")

async def OnReceived(message):
    global master_word
    global master_Norm
    if message.author.bot:
        return
    word = message.content
    if not master_word and word.startswith("GOAL=||") and word.endswith("||"):
        word = word[7:-2]
        master_word = word
        nTuple = ((await utils.llm.GetNearWords(master_word))[0])
        master_Norm = nTuple[1]
        words.clear()
        utils.console.SendText("RESET")
        await utils.mydiscord.print_log(f"この単語をゴールの単語に決定しました。\n目標Norm={master_Norm}")
        return
    if master_word and word == "ヒント":
        await message.reply(f"ヒント:\n```{await utils.llm.GetNearWords(master_word)}```")
    if master_word:
        wordObj = await utils.llm.Norm(master_word, word)
        words.append(wordObj)
        words.sort()
        norm = wordObj.Norm
        perf = norm / master_Norm
        utils.console.SendArray(filter(lambda x : x.Word, words[:5]))
        if norm > 0:
            await message.reply(f"{OutputMessage(perf)} 一致率:{round(perf*100)}%")
        else:
            await message.reply("未知の単語です...")
        if norm >= 0.999999:
            master_word = ""
            utils.console.SendText("ANSWER")
            await utils.mydiscord.print_log("正解しました。ゴールの単語を再設定してください。")

utils.mydiscord.Inited = OnInited
utils.mydiscord.Received = OnReceived
utils.mydiscord.Client.run(TOKEN)