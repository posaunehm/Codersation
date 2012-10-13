/*
 * JuiceStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "JuiceStock.h"
#include "Juice.h"

//これをコンストラクタとする
JuiceStock::JuiceStock(const Juice& juice, int count)
:count_(count)
{
  pJuice_ = new Juice(juice);
}

//C++11のインストールできないから、使わないけどデフォルトコンストラクタの記載しておく
JuiceStock::JuiceStock()
:pJuice_(0),count_(0)
{

}

JuiceStock::~JuiceStock()
{
  delete pJuice_;
}

