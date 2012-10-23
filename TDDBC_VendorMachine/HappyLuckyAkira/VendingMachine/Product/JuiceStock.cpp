/*
 * JuiceStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "JuiceStock.h"
#include "Juice.h"
#include "JuiceRack.h"
#include <boost/make_shared.hpp>
#include <functional>
#include <algorithm>

//これをコンストラクタとする
JuiceStock::JuiceStock(const Juice& juice, int count)
{
  boost::shared_ptr<JuiceRack> pJuiceRack = boost::make_shared<JuiceRack>(juice,count);
  juicerack_list_.push_back(pJuiceRack);

}
bool
JuiceStock::IsInStock(const Juice& juice)
{
  std::list<JuiceRackPointer>::iterator juicerackiterator;
  juicerackiterator = find_if(juicerack_list_.begin(), juicerack_list_.end(),
      std::bind2nd( IsInRack(), juice )     // バインダと共に使う(juice)
  );
  if (juicerackiterator == juicerack_list_.end()){return false;}
  return true;
}

//C++11のインストールできないから、使わないけどデフォルトコンストラクタの記載しておく
JuiceStock::JuiceStock()
{

}

JuiceStock::~JuiceStock()
{
}

bool
IsInRack::operator ()(const boost::shared_ptr<JuiceRack> pjuicerack, const Juice& juice) const
{
  if (pjuicerack->getJuice() == juice) {return true;};
  return false;
}


