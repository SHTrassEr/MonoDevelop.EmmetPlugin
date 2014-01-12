#! /bin/bash -e

echo ""
echo "MonoDevelop Makefile configuration script"
echo "-----------------------------------------"
echo "This will generate Makefile with correct paths for you."
echo ""
echo "Usage: ./configure.sh"
echo ""


# ------------------------------------------------------------------------------
# Utility function that searches specified directories for a specified file
# and if the file is not found, it asks user to provide a directory

RESULT=""

searchpaths()
{
  declare -a DIRS=("${!3}")
  FILE=$2
  DIR=${DIRS[0]}

  for TRYDIR in "${DIRS[@]}"
  do
    if [ -f "$TRYDIR"/$FILE ] 
    then
      DIR=$TRYDIR
    fi
  done

  if [ ! -f "$DIR"/$FILE ]
  then 
    echo "Warning: File '$FILE' was not found in any of ${DIRS[@]}."
    echo "Please enter path to '$FILE':"
    read DIR
  fi
  RESULT=$DIR
}

# ------------------------------------------------------------------------------
# Find all paths that we need in order to generate the make file. Paths
# later in the list are preferred.

MONO=mono

if ! hash $MONO 2>/dev/null;  then
  echo "Warning: Mono was not found. Please enter path to mono."
  read MONO
fi

if hash $MONO 2>/dev/null;  then
  echo "Successfully found $MONO"
else 
  echo "Warning: Mono was not found. Please enter path to mono."
  read MONO

  if [ -f $MONO ]
  then
    echo "Successfully found $MONO"
  else
    echo "Configuration error. Mono was not found."
    exit
  fi
fi

PATHS=( 
  /opt/mono/lib/monodevelop/bin 
  /usr/lib/monodevelop/bin 
  /usr/local/monodevelop/lib/monodevelop/bin 
  /usr/local/lib/monodevelop/bin 
  /Applications/MonoDevelop.app/Contents/MacOS/lib/monodevelop/bin 
  /Applications/Xamarin Studio.app/Contents/MacOS/lib/monodevelop/bin
  )
searchpaths "MonoDevelop" MonoDevelop.Core.dll PATHS[@]
MDDIR=$RESULT

if [ -f $MDDIR/MonoDevelop.Core.dll ]
then
  echo "Successfully found MonoDevelop bin directory: $MDDIR"
else
  echo "Configuration error. File $MDDIR/MonoDevelop.Core.dll was not found."
  exit
fi

MDRUNFILE="$MDDIR/MonoDevelop.exe"

if [ -f "$MDRUNFILE" ] 
then 
  echo "Running $MONO $MDRUNFILE to determine MonoDevelop version"
  # e.g. 3.0.4.7
  MDVERSION=`$MONO $MDRUNFILE /? | head -n 1 | grep -o "[0-9]\+.[0-9]\+\(.[0-9]\+\)\?"`
  echo "Detected MonoDevelop version " $MDVERSION
else
  MDVERSION=4.0
  echo "Assumed MonoDevelop version " $MDVERSION
fi

PATHS=( $MDDIR )

searchpaths "mdtool" mdtool.exe PATHS[@]
MDTOOL=$RESULT/mdtool.exe
if [ -f "$MDTOOL" ] 
then 
  echo "Successfully found mdtool.exe file: $MDTOOL"
else
  echo "Configuration error. File $MDTOOL not found."
  exit
fi

MDTOOL="$MONO $MDTOOL"

# ------------------------------------------------------------------------------
# Write Makefile

sed -e "s,INSERT_MAKE_MDROOT,$MDDIR,g" -e "s,INSERT_MAKE_MDTOOL,$MDTOOL,g" -e "s,INSERT_MAKE_MDVERSION,$MDVERSION,g" Makefile.orig > Makefile 
