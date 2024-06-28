def SendText(text):
    print(f"##{text}##")

def SendArray(ary):
    text = ""
    First = True
    for a in ary:
        if First:
            text += str(a)
            First = False
        else:
            text += f"|{a}"
    SendText(text)