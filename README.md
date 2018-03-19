# ProjectX
Under Construction!


ProjectX is an effort to gather data from original Xbox games.


Data collection will include xbe metadata, file lists of retail games, tools to unpack and repack archives from games, and lists of unpacked files.


XbeTool can be used to generated wiki (markdown) entries for xbe files.


A protocol will be put into place to search for any interesting (debug) material left behind in games. One such example is using `grep` to search for terms like `debug`, `demo`, `test`, `http:\\`, `funny swears`, `Dr. Phil`, etc.

Before contribution can begin, these things need to be done:

	wiki structure needs to be semi-finalized
	
	An actual database needs to be setup to hold all of this information. Github and markdown are great, but I'd like something more portable.
	
	Need to differentiate builds
	
	Autodetection / best guess of file types
	
	Validation of executables with public key (Debug builds still welcome!)
	
	Finalization of debug content search protocol

Check out the games folder to see where this is headed.