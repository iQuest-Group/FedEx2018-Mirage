# -*- coding: utf-8 -*-
"""
Created on Mon Nov  5 14:34:53 2018

@author: adrian.gogolan
"""

import matplotlib.pyplot as plt, IPython.display as ipd
import librosa, librosa.display
import stanford_mir; stanford_mir.init()
import argparse

parser = argparse.ArgumentParser(description="Sets up the build by creating the necessary configuration files", epilog="")
parser.add_argument('--sourceFile', '-s', help="path to folder containing the configuration files to be copied")
arguments = parser.parse_args()

audio_file = arguments.sourceFile
#audio_file = input('audio/simple_piano.wav')
#audio_file = input('audio file:')


#x, sr = librosa.load('audio/simple_piano.wav')
x, sr = librosa.load(audio_file)
ipd.Audio(x, rate=sr)

fmin = librosa.midi_to_hz(36)
hop_length = 512
C = librosa.cqt(x, sr=sr, fmin=fmin, n_bins=72, hop_length=hop_length)

chromagram = librosa.feature.chroma_stft(x, sr=sr, hop_length=hop_length)
#plt.figure(figsize=(15, 5))
#librosa.display.specshow(chromagram, x_axis='time', y_axis='chroma', hop_length=hop_length, cmap='coolwarm')

print(chromagram.tolist())

#with open('output', 'w') as f:
 #  numpy.savetxt(f, chromagram)