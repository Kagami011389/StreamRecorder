song_list = []
t_list = []
txt = open('songlist.txt',encoding='UTF-8')
song = txt.read()
txt.close()
sline = song.split('\n')
for n in sline:
    t_list.append(n[:8])
    song_list.append(n[8:])
print(sline)

h_list = []
m_list = []
s_list = []
s_shift = 10
for t in t_list:

    h = t.split(":")[0]
    m = t.split(":")[1]
    s = t.split(":")[2]
    h_list.append(int(h))
    m_list.append(int(m))
    s_list.append(int(s))

for i in range(len(h_list)):
    s_list[i] += s_shift
    if s_list[i] < 0:
        m_list[i] -= 1
        s_list[i] += 60

    if s_list[i] >= 60:
        m_list[i] += 1
        s_list[i] -= 60

    if m_list[i] < 0:
        m_list[i] += 60
        h_list[i] -= 1

    if m_list[i] >= 60:
        m_list[i] -= 60
        h_list[i] += 1

for i in range(len(h_list)):
    print(f"{str(h_list[i]).zfill(2)}:{str(m_list[i]).zfill(2)}:{str(s_list[i]).zfill(2)} {song_list[i]}")
