import urllib,time
import pymsgbox as ms
import winsound
global tagReq
def getTag():

    tagReq = ms.prompt("Please Enter the same tag that you entered in your phone","Enter The tag","4 character tag ",None,None)
    if(len(tagReq.strip()) != 4):
        getTag()
    else:
        return tagReq
tagrequest= getTag()
print tagrequest
last_state="Idle"
global last_num
last_num = " "
state=""
Number = ""
global actiontxt
actiontxt=""


def generateui(state,number):
    global last_state
    if(last_state == state):
        winsound.PlaySound(None,winsound.SND_ASYNC)
        return
    elif(last_state == "Incoming" or last_state == "Outgoing" and state == "Idle"):
        winsound.PlaySound("nlla.mp3", winsound.SND_ASYNC)
        ms.alert(number,state,"Call Disconnected",None,5000)
    else:
        if(state == "Incoming"):
            actiontxt = "Go Answer!"
            winsound.PlaySound(None, winsound.SND_ASYNC)
            winsound.PlaySound("BuildItUp1.wav", winsound.SND_ASYNC | winsound.SND_ALIAS)
        if (state == "Outgoing"):
            actiontxt = "call picked"
            winsound.PlaySound(None, winsound.SND_ASYNC)
            winsound.PlaySound("outgoing.wav", winsound.SND_ASYNC | winsound.SND_ALIAS)
        if(state == "answering"):
            actiontxt = "You are Talking To "+number
            winsound.PlaySound(None, winsound.SND_ASYNC)

        global actiontxt
        ms.alert(number, state, actiontxt, None, 30000)

    last_state = state






while 1:
    url = "https://autolligentbeta.000webhostapp.com/ardaction.php"

    time_beg = time.time()
    try:
        f = urllib.urlopen(url)
    except Exception,e:
        print "error while opening the Url"
        if(str(e) == "[Errno socket error] [Errno 11001] getaddrinfo failed"):
            print "Please Check Your Internet Connection"
        print e
        pass

    if(f.code == 200):
        try:
            state,Number,Tag= f.read().strip().split()
            #print Tag
            if(Tag):
                pass
            else:
                ms.alert("Please Set the Tag Id In your Phone app", "Warning !!", "Ok", None, 10000)
                exit(0)
            if(Tag !=tagrequest):
                tagrequest=getTag()

            time_end = time.time()
            if(Number == "__"):

                Number = last_num


        except (ValueError):
            Number = last_num
        finally:
            #print state,Number
            last_num = Number
            generateui(state,last_num)
    elif (f.code==404):
        print "Please Check the Url"
    else:
        print f.code