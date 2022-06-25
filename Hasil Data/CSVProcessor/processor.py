from pathlib import Path
import os
from dictionary import *
import csv
import matplotlib.pyplot as plt
import glob
from model import Person

currentpath = Path(os.getcwd())

paths = (currentpath.parent).glob("Adam*.csv")
files = []
for file in paths:
    files.append(file)
Adam = Person(files[0], files[1], files[2], "03:49", "07:39", "11:39")
Adam.ToCSV(currentpath.joinpath("processed/Adam-processed.csv"))

paths = (currentpath.parent).glob("Maisie*.csv")
files = []
for file in paths:
    files.append(file)
Maisie = Person(files[0], files[1], files[2], "03:39", "01:51", "01:56")
Maisie.ToCSV(currentpath.joinpath("processed/Maisie-processed.csv"))

paths = (currentpath.parent).glob("Ahdan*.csv")
files = []
for file in paths:
    files.append(file)
Ahdan = Person(files[0], files[1], files[2], "03:39", "01:35", "04:09")
Ahdan.ToCSV(currentpath.joinpath("processed/Ahdan-processed.csv"))

paths = (currentpath.parent).glob("Gerry*.csv")
files = []
for file in paths:
    files.append(file)
Gerry = Person(files[0], files[1], files[2], "10:09", "02:39", "05:39")
Gerry.ToCSV(currentpath.joinpath("processed/Gerry-processed.csv"))

paths = (currentpath.parent).glob("Anggun*.csv")
files = []
for file in paths:
    files.append(file)
Anggun = Person(files[0], files[1], files[2], "06:39", "03:09", "04:39")
Anggun.ToCSV(currentpath.joinpath("processed/Anggun-processed.csv"))

paths = (currentpath.parent).glob("Tari*.csv")
files = []
for file in paths:
    files.append(file)
Tari = Person(files[0], files[1], files[2], "06:09", "04:25", "03:09")
Tari.ToCSV(currentpath.joinpath("processed/Tari-processed.csv"))

paths = (currentpath.parent).glob("Dicky*.csv")
files = []
for file in paths:
    files.append(file)
Dicky = Person(files[0], files[1], files[2], "02:09", "05:07", "07:09")
Dicky.ToCSV(currentpath.joinpath("processed/Dicky-processed.csv"))

paths = (currentpath.parent).glob("Zakiya*.csv")
files = []
for file in paths:
    files.append(file)
Zakiya = Person(files[0], files[1], files[2], "02:01", "06:09", "02:09")
Zakiya.ToCSV(currentpath.joinpath("processed/Zakiya-processed.csv"))

paths = (currentpath.parent).glob("Dede*.csv")
files = []
for file in paths:
    files.append(file)
Dede = Person(files[0], files[1], files[2], "02:39", "02:09", "02:39")
Dede.ToCSV(currentpath.joinpath("processed/Dede-processed.csv"))

paths = (currentpath.parent).glob("Irsyad*.csv")
files = []
for file in paths:
    files.append(file)
Irsyad = Person(files[0], files[1], files[2], "04:39", "04:31", "03:41")
Irsyad.ToCSV(currentpath.joinpath("processed/Irsyad-processed.csv"))

paths = (currentpath.parent).glob("Elin*.csv")
files = []
for file in paths:
    files.append(file)
Elin = Person(files[0], files[1], files[2], "02:39", "02:09", "04:09")
Elin.ToCSV(currentpath.joinpath("processed/Elin-processed.csv"))

paths = (currentpath.parent).glob("Intan*.csv")
files = []
for file in paths:
    files.append(file)
Intan = Person(files[0], files[1], files[2], "04:17", "02:53", "06:39")
Intan.ToCSV(currentpath.joinpath("processed/Intan-processed.csv"))
