/*
 * JuiceStock.h
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#ifndef JUICESTOCK_H_
#define JUICESTOCK_H_

class Juice;

class JuiceStock
{
public:
  explicit JuiceStock(const Juice& juice, int count);
  virtual
  ~JuiceStock();

    int getCount() const
    {
        return count_;
    }
    const Juice& getJuice() const
    {
        return *pJuice_;
    }


private:
    JuiceStock();
    Juice* pJuice_;
    int count_;
};

#endif /* JUICESTOCK_H_ */
