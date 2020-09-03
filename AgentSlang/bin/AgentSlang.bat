@if "%DEBUG%" == "" @echo off
@rem ##########################################################################
@rem
@rem  AgentSlang startup script for Windows
@rem
@rem ##########################################################################

@rem Set local scope for the variables with windows NT shell
if "%OS%"=="Windows_NT" setlocal

@rem Add default JVM options here. You can also use JAVA_OPTS and AGENT_SLANG_OPTS to pass JVM options to this script.
set DEFAULT_JVM_OPTS=

set DIRNAME=%~dp0
if "%DIRNAME%" == "" set DIRNAME=.
set APP_BASE_NAME=%~n0
set APP_HOME=%DIRNAME%..
set PATH=%PATH%;%APP_HOME%\lib

@rem Find java.exe
if defined JAVA_HOME goto findJavaFromJavaHome

set JAVA_EXE=java.exe
%JAVA_EXE% -version >NUL 2>&1
if "%ERRORLEVEL%" == "0" goto init

echo.
echo ERROR: JAVA_HOME is not set and no 'java' command could be found in your PATH.
echo.
echo Please set the JAVA_HOME variable in your environment to match the
echo location of your Java installation.

goto fail

:findJavaFromJavaHome
set JAVA_HOME=%JAVA_HOME:"=%
set JAVA_EXE=%JAVA_HOME%/bin/java.exe

if exist "%JAVA_EXE%" goto init

echo.
echo ERROR: JAVA_HOME is set to an invalid directory: %JAVA_HOME%
echo.
echo Please set the JAVA_HOME variable in your environment to match the
echo location of your Java installation.

goto fail

:init
@rem Get command-line arguments, handling Windowz variants

if not "%OS%" == "Windows_NT" goto win9xME_args
if "%@eval[2+2]" == "4" goto 4NT_args

:win9xME_args
@rem Slurp the command line arguments.
set CMD_LINE_ARGS=
set _SKIP=2

:win9xME_args_slurp
if "x%~1" == "x" goto execute

set CMD_LINE_ARGS=%*
goto execute

:4NT_args
@rem Get arguments from the 4NT Shell from JP Software
set CMD_LINE_ARGS=%$

:execute
@rem Setup the command line

set CLASSPATH=%APP_HOME%\lib\AgentSlang.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\babelnet-api-2.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jlt-1.0.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\ACT-R.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\cerevoice_eng.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\iSpeechSDK_9.26.12.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\weka-stable-3.6.10.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\MyBlock.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\Synnbad.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-codec-1.10.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jdom2-2.0.6.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-http-server-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-websockets-server-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\selenium-chrome-driver-2.47.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\bluecove-2.1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\bluecove-gpl-2.1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\httpclient-cache-4.5.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-runtime-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-io-2.4.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\javolution-core-java-6.0.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-core-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-lang-2.6.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jgraphx-3.3.1.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-de-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-en-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-it-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-ru-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-sv-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-te-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-lang-tr-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\voice-cmu-slt-hsmm-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jcommander-1.48.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jeromq-0.3.5.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\msgpack-0.6.12.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\reflections-0.9.10.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\miglayout-swing-5.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\gs-core-1.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jwi-2.2.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\extjwnl-1.9.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-http-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-http-server-core-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-websockets-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\selenium-remote-driver-2.47.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\httpclient-4.5.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-logging-1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-common-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\marytts-signalproc-5.1.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-collections-3.2.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\emotionml-checker-java-1.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jtok-core-1.9.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\freetts-1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\freetts-de-1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\freetts-en_us-1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\trove4j-2.0.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\httpcore-nio-4.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\opennlp-maxent-3.0.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\opennlp-tools-1.5.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\hsqldb-2.0.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\org.osgi.core-4.3.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\org.osgi.compendium-4.3.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-autocomplete-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-action-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-plaf-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-graphics-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-painters-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\json-simple-1.1.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\annotations-2.0.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\miglayout-core-5.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\junit-4.12.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\pherd-1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\mbox2-1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\slf4j-api-1.7.12.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\slf4j-log4j12-1.7.12.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\concurrentlinkedhashmap-lru-1.3.2.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-framework-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-core-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-http-ajp-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-spdy-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-npn-api-1.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-http-server-multipart-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\cglib-nodep-2.1_3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\gson-2.3.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\selenium-api-2.47.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\commons-exec-1.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jna-4.1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jna-platform-4.1.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swing-layout-1.0.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\jwnl-1.3.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\swingx-common-1.6.5-1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\hamcrest-core-1.3.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\grizzly-portunif-2.3.23.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\guava-18.0.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\javassist-3.18.2-GA.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\httpcore-4.4.1.jar
set CLASSPATH=%CLASSPATH%;%APP_HOME%\lib\log4j-1.2.17.jar

@rem Execute AgentSlang
"%JAVA_EXE%" %DEFAULT_JVM_OPTS% %JAVA_OPTS% %AGENT_SLANG_OPTS%  org.ib.component.ComponentRunner %CMD_LINE_ARGS%

:end
@rem End local scope for the variables with windows NT shell
if "%ERRORLEVEL%"=="0" goto mainEnd

:fail
rem Set variable AGENT_SLANG_EXIT_CONSOLE if you need the _script_ return code instead of
rem the _cmd.exe /c_ return code!
if  not "" == "%AGENT_SLANG_EXIT_CONSOLE%" exit 1
exit /b 1

:mainEnd
if "%OS%"=="Windows_NT" endlocal

:omega
