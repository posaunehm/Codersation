/*
 * JuiceRack.h
 *
 *  Created on: 2012/10/13
 *      Author: Akira
 */

#ifndef JUICERACK_H_
#define JUICERACK_H_

class Juice;
class JuiceRack
{
public:
  explicit JuiceRack(const Juice& juice, int count);
  void remove(void);
  virtual
  ~JuiceRack();

  int
  getCount() const
  {
    return count_;
  }

  const Juice&
  getJuice() const
  {
    return *pJuice_;
  }
  bool Of(const Juice& juice);
private:
  JuiceRack();
  Juice* pJuice_;
  int count_;
};

#endif /* JUICERACK_H_ */
