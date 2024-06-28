import discord

Client = discord.Client(intents=discord.Intents.all())
Initialized = False

Inited = None
Received = None

m_status = ""

admin_message = None

@Client.event
async def on_ready():
    print('ログインしました')
    global Initialized
    Initialized = True
    await Client.change_presence(activity=discord.Game(name=m_status))
    

@Client.event
async def on_message(message):
    global admin_message
    if message.content == "::exit::":
        exit()
    if admin_message is None and message.content == "::init::":
        admin_message = message
        await Inited()
    await Received(message)
    
async def set_Status(text):
    global m_status
    m_status = text
    if Initialized:
        await Client.change_presence(activity=discord.Game(name=m_status))

async def print_log(text):
    if admin_message is None:
        print(text)
    await admin_message.reply(text)