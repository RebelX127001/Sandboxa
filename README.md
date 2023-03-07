# Sandboxa

A simple Sandboxing tool for running untrusted programs.

Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

Prerequisites

What things you need to install the software and how to install them:

    Get visual studio community or enterprise edition.
    Import the solution
    Build solution

Installation

A step by step series of examples that tell you how to get a development environment running.

shell

$ git clone https://github.com/<username>/<project>.git
    
$ mv <project> <visul studio repo directory>


Usage

How to use the software:

    Get the exe
    $cd <exe-path>
    $sandboxa /? - for help
    

Permissions

This project includes UI Checkboxes and flags (if you are running it directly from the terminal) for granting permissions. Here's a list of the available permissions and what they allow:

FOR USAGE ON A TERMINAL

    Permissions               Flags
    
    Read/Write Access:         -f
    Network Access:            -n
    Execution Access:          -e
    UI Access:                 -ui
    File Manager:              -fm
    Clipboard Access:          -cb

    Usage 1: $sandboxa <filepath> <filename> <flag>
    You can also combine flags.
    
FOR USAGE VIA GUI
    
    $sandboxa start.  --Launches the GUI version of Sandboxa
