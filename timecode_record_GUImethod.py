from PyQt6.QtWidgets import QApplication,QMainWindow
from timecode_record_1 import Ui_MainWindow
import timeprocess
import sys

class UIM(Ui_MainWindow):
    def __init__(self):
        super(UIM, self).__init__()
        self.t = timeprocess.timecode()
        self.pushButton.clicked.connect(self.PrintTimeCode)
    def PrintTimeCode(self):
        timecode = self.t.getTimecode(self.lineEdit.text(), self.lineEdit_2.text(), self.lineEdit_3.text(), self.lineEdit_4.text())
        self.textEdit.append(timecode)
        # print(self.lineEdit.text(),self.lineEdit_2.text(),self.lineEdit_3.text(),self.lineEdit_4.text())

if __name__ == '__main__':
    App = QApplication(sys.argv)
    App.setStyle('fusion')
    window = UIM()
    window.show()
    # window.setStyleSheet('Fusion')
    App.exec()