import time

class timecode:
    def __init__(self):
        self.stream_year = 2021
        self.stream_month = 1
        self.stream_day = 1
        self.stream_hour = 1
        self.stream_min = 1
        self.stream_sec = 1
    def getTimecode(self,year=2021,month=6,day=17,shour="07:30"):
        self.stream_hour = int(shour.split(":")[0])
        self.stream_min = int(shour.split(":")[1])
        self.stream_day = int(day)
        self.stream_month = int(month)
        self.stream_year = int(year)
        timecode = [time.localtime()[0] - self.stream_year,
                    time.localtime()[1] - self.stream_month,
                    time.localtime()[2] - self.stream_day,
                    time.localtime()[3] - self.stream_hour,
                    time.localtime()[4] - self.stream_min,
                    time.localtime()[5]]
        for n in range(5):
            if timecode[3] < 0:
                timecode[2] -= 1
                timecode[3] += 24
            if timecode[4] < 0:
                timecode[3] -= 1
                timecode[4] += 60
            if timecode[5] < 0:
                timecode[4] -= 1
                timecode[5] += 60

        timecode = f"{str(timecode[3]).zfill(2)}:{str(timecode[4]).zfill(2)}:{str(timecode[5]).zfill(2)}"
        return timecode



