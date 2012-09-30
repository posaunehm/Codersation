/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JuiceStock {
    private Juice juice;
    private Integer count;
    
    public JuiceStock(Juice juice, Integer count) {
        this.juice = juice;
        this.count = count;
    }

    public Juice getJuice() {
        return juice;
    }

    public void setJuice(Juice juice) {
        this.juice = juice;
    }

    public Integer getCount() {
        return count;
    }

    public void setCount(Integer count) {
        this.count = count;
    }
    
    @Override
    public boolean equals(Object that) {
        if(that instanceof JuiceStock) {
            JuiceStock thatStock = (JuiceStock)that;
            if(this.juice.equals(thatStock.getJuice())
                    && this.count.equals(thatStock.getCount())) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
}
