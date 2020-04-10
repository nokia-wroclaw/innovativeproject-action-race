#! /bin/sh

if [[ -z "${BUTLER_API_KEY}" ]]; then
  echo "Unable to deploy! No BUTLER_API_KEY environment variable specified!"
  exit 1
fi

prepare_butler() {
    echo "Preparing butler..."
    download_if_not_exist https://broth.itch.ovh/butler/darwin-amd64/LATEST/archive/default butler.zip
    unzip butler.zip
    chmod +x butler
}

prepare_and_push() {
    echo "Push $3 build to itch.io..."
    ./butler push $2 $1:$3
}

download_if_not_exist() {
    if [ ! -f $2 ]; then
        curl -L -o $2 $1
    fi
}

echo "jestem w $(pwd)"

project="tomkul777/nokia-game"
artifact="nokia-game.zip"
platform="windows-beta"

prepare_butler

prepare_and_push $project $artifact $platform

echo "Done."
exit 0