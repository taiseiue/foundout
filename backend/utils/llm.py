import gensim

Model = None
Initialized = False

async def Load(fileName):
    global Model
    Model= gensim.models.KeyedVectors.load_word2vec_format(fileName, binary=False)
    global Initialized
    Initialized = True

async def Norm(wordA, wordB):
    result = Word()
    result.Word = wordB
    try:
        result.Norm =  Model.similarity(wordA,wordB)
    except:
        result.Norm =  0
    return result

async def GetNearWords(word):
    return Model.most_similar(word)


class Word:
    Word = ""
    Norm = 0

    def __lt__(self, other):
        return self.Norm > other.Norm
    def __str__(self) -> str:
        return self.Word