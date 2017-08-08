echo "PATH ======= $PATH"
echo "WORKSPACE ======= $WORKSPACE"

cd .. && mv s monodevelop && cd monodevelop

if [ "$1" = "checkout" ]; then 
    rake -f $WORKSPACE/wrench/monodevelop/Rakefile checkout 
else
    make $1
fi;

cd .. && mv monodevelop s && cd s
