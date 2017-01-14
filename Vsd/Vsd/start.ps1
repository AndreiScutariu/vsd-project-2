#arg 0: slave key id
#arg 1: draw type
#arg 2: draw rotation 0(0x 1y 0z), 1(-1x 1y 0z) ...
#arg 3: draw color 1-red, 2-green, 3-blue

#Draw
Start-Process .\Vsd.Slave.Drawing\bin\Debug\Vsd.Slave.Drawing.exe 1-1101
#Start-Process .\Vsd.Slave.Drawing\bin\Debug\Vsd.Slave.Drawing.exe 2-2122

#Aggreagator
#Start-Process .\Vsd.Slave.Node\bin\Debug\Vsd.Slave.Node.exe 1-44441-44442-44445 > node1.txt
#Start-Process .\Vsd.Slave.Node\bin\Debug\Vsd.Slave.Node.exe 2-44443-44444-44446 > node2.txt

#Master
Start-Process .\Vsd.Master\bin\Debug\Vsd.Master.exe 1 	