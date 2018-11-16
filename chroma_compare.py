# -*- coding: utf-8 -*-
"""
Created on Mon Nov  5 14:34:53 2018

@author: adrian.gogolan
"""

import numpy, scipy, matplotlib.pyplot as plt, IPython.display as ipd
import librosa, librosa.display
import stanford_mir; stanford_mir.init()

x, sr = librosa.load('audio/simple_piano.wav')
ipd.Audio(x, rate=sr)

fmin = librosa.midi_to_hz(36)
hop_length = 512
C = librosa.cqt(x, sr=sr, fmin=fmin, n_bins=72, hop_length=hop_length)

chromagram = librosa.feature.chroma_stft(x, sr=sr, hop_length=hop_length)
plt.figure(figsize=(15, 5))
librosa.display.specshow(chromagram, x_axis='time', y_axis='chroma', hop_length=hop_length, cmap='coolwarm')


print (chromagram[0,0])