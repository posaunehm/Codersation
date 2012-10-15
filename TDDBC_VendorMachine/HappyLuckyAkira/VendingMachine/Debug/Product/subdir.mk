################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
CPP_SRCS += \
../Product/Amount.cpp \
../Product/Juice.cpp \
../Product/JuiceRack.cpp \
../Product/JuiceStock.cpp \
../Product/Money.cpp \
../Product/MoneyStock.cpp \
../Product/VendingMachine.cpp 

OBJS += \
./Product/Amount.o \
./Product/Juice.o \
./Product/JuiceRack.o \
./Product/JuiceStock.o \
./Product/Money.o \
./Product/MoneyStock.o \
./Product/VendingMachine.o 

CPP_DEPS += \
./Product/Amount.d \
./Product/Juice.d \
./Product/JuiceRack.d \
./Product/JuiceStock.d \
./Product/Money.d \
./Product/MoneyStock.d \
./Product/VendingMachine.d 


# Each subdirectory must supply rules for building sources it contributes
Product/%.o: ../Product/%.cpp
	@echo 'Building file: $<'
	@echo 'Invoking: Cygwin C++ Compiler'
	g++ -I"C:\Indigo\workspace\CppUTest-v3.3\include" -I/cygdrive/c/Indigo/workspace/CppUTest-v3.3/include -O0 -g3 -Wall -c -fmessage-length=0 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


