#! /bin/sh

UNITY_DOWNLOAD_CACHE="$(pwd)/unity_download_cache"
UNITY_OSX_PACKAGE_URL="https://download.unity3d.com/download_unity/46dda1414e51/MacEditorInstaller/Unity-2017.2.0f3.pkg"
UNITY_WEBGL_URL="https://beta.unity3d.com/download/46dda1414e51/MacEditorTargetInstaller/UnitySetup-WebGL-Support-for-Editor-2017.2.0f3.pkg"

#UNITY_OSX_PACKAGE_URL="https://download.unity3d.com/download_unity/4f139db2fdbd/MacEditorInstaller/Unity.pkg"
#UNITY_WEBGL_URL="https://download.unity3d.com/download_unity/4f139db2fdbd/MacEditorTargetInstaller/UnitySetup-WebGL-Support-for-Editor-2019.3.4f1.pkg"

# Downloads a file if it does not exist
download() {

	URL=$1
	FILE=`basename "$URL"`
	
	# Downloads a package if it does not already exist in cache
	if [ ! -e $UNITY_DOWNLOAD_CACHE/`basename "$URL"` ] ; then
		echo "$FILE does not exist. Downloading from $URL: "
		mkdir -p "$UNITY_DOWNLOAD_CACHE"
		curl -o $UNITY_DOWNLOAD_CACHE/`basename "$URL"` "$URL"
	else
		echo "$FILE Exists. Skipping download."
	fi
}

# Downloads and installs a package from an internet URL
install() {
	PACKAGE_URL=$1
	download $1

	echo "Installing `basename "$PACKAGE_URL"`"
	sudo installer -dumplog -package $UNITY_DOWNLOAD_CACHE/`basename "$PACKAGE_URL"` -target /
}

echo "Installing Unity..."
install $UNITY_OSX_PACKAGE_URL
install $UNITY_WEBGL_URL
