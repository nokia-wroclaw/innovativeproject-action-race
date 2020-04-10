#! /bin/sh

PROJECT_PATH="$(pwd)/$UNITY_PROJECT_PATH"
WEBGL_DIR="$PROJECT_PATH/Build/WebGL"

echo "Building WebGL..."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$(PROJECT_PATH)" \
  -executeMethod AppBuilder.Build \
  -quit

echo "Zip WebGL files..."
zip -r nokia-game.zip "$WEBGL_DIR/Build" "$WEBGL_DIR/TemplateData" "$WEBGL_DIR/index.html"
