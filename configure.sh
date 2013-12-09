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
    echo "Warning: File '$FILE' was not found in any of ${DIRS[@]}. Please enter path to monodevelop."
    read DIR
  fi
  RESULT=$DIR
}

# ------------------------------------------------------------------------------
# Find all paths that we need in order to generate the make file. Paths
# later in the list are preferred.

PATHS=( /opt/mono/lib/monodevelop /usr/lib/monodevelop /usr/local/monodevelop/lib/monodevelop /usr/local/lib/monodevelop /Applications/MonoDevelop.app/Contents/MacOS/lib/monodevelop '/Applications/Xamarin Studio.app/Contents/MacOS/lib/monodevelop')
searchpaths "MonoDevelop" bin/MonoDevelop.Core.dll PATHS[@]
MDDIR=$RESULT
echo "Successfully found MonoDevelop root directory." $MDDIR

echo "Running $MDDIR/monodevelop to determine MonoDevelop version"

if [  -f "$MDDIR"/monodevelop ] 
then 
  # e.g. 3.0.4.7
  MDVERSION4=`$MDDIR/bin/monodevelop /? | head -n 1 | grep -o "[0-9]\+.[0-9]\+.[0-9]\+\(.[0-9]\+\)\?"`
  # e.g. 3.0.4
  MDVERSION3=`$MDDIR/bin/monodevelop /? | head -n 1 | grep -o "[0-9]\+.[0-9]\+.[0-9]\+"`
  echo "Detected MonoDevelop version " $MDVERSION4
else
  MDVERSION4=4.0.0.0
  MDVERSION3=4.0
  echo "Assumed MonoDevelop version " $MDVERSION4
fi


# ------------------------------------------------------------------------------
# Write Makefile

sed -e "s,INSERT_MAKE_MDROOT,$MDDIR,g" -e "s,INSERT_MAKE_MDVERSION3,$MDVERSION3,g" -e "s,INSERT_MAKE_MDVERSION4,$MDVERSION4,g" Makefile.orig > Makefile 
