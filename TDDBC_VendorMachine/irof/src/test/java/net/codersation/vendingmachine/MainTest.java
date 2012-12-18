package net.codersation.vendingmachine;

import org.junit.Ignore;
import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.TestWatcher;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.io.StringReader;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.CoreMatchers.nullValue;
import static org.junit.Assert.assertThat;

public class MainTest {

    @Rule
    public SystemOut out = new SystemOut();
    public SystemIn in = SystemIn.create();

    @Test
    public void 入力待ちを表示する() throws Exception {
        Main.main();
        assertThat(out.readLine(), is("> "));
    }

    @Test
    public void insでお金を入れるとメッセージが出る() throws Exception {
        in.writeLine("ins 100");
        in.writeLine("ins 10");

        Main.main();

        assertThat(out.readMassage(), is("money: 100 was received."));
        assertThat(out.readMassage(), is("money: 10 was received."));
    }

    @Test
    public void insで続けただけお金が入る() throws Exception {
        in.writeLine("ins 100 100 10 100");

        Main.main();

        assertThat(out.readMassage(), is("money: 100 was received."));
        assertThat(out.readMassage(), is("money: 100 was received."));
        assertThat(out.readMassage(), is("money: 10 was received."));
        assertThat(out.readMassage(), is("money: 100 was received."));
    }

    @Test
    public void 変な金額をinsしたらエラーメッセージ() throws Exception {
        in.writeLine("ins 123");
        Main.main();
        assertThat(out.readMassage(), is("! 123 is not available."));
    }

    @Test
    public void 変なものをinsしたらエラーメッセージ() throws Exception {
        in.writeLine("ins hoge");
        Main.main();
        assertThat(out.readMassage(), is("! hoge is not available."));
    }

    @Test
    public void ejectで出てくる() throws Exception {
        in.writeLine("ins 100");
        in.writeLine("eject");
        Main.main();

        out.readMassage();
        assertThat(out.readMassage(), is("100(1) was ejected."));
    }

    @Test
    public void ejectで二枚出す() throws Exception {
        in.writeLine("ins 100");
        in.writeLine("ins 500");
        in.writeLine("eject");
        Main.main();

        out.readMassage();
        out.readMassage();
        assertThat(out.readMassage(), is("100(1), 500(1) was ejected."));
    }

    @Test
    public void exitで終了する() throws Exception {
        in.writeLine("ins 100");
        in.writeLine("exit");
        in.writeLine("ins 500");
        Main.main();

        out.readMassage();
        assertThat(out.readMassage(), is(nullValue()));
    }

    @Test
    public void infoで現在の投入金額を表示() throws Exception {
        in.writeLine("ins 100");
        in.writeLine("ins 50");
        in.writeLine("info");
        Main.main();

        out.readMassage();
        out.readMassage();
        assertThat(out.readMassage(), is("Credit:150"));
        assertThat(out.readMassage(), is("[コーラ, 水]"));
    }

    static class SystemIn {
        StringBuilder sb = new StringBuilder();

        public void writeLine(String str) {
            sb.append(str);
            sb.append(System.lineSeparator());
            System.setIn(new ByteArrayInputStream(sb.toString().getBytes()));
        }

        public static SystemIn create() {
            SystemIn instance = new SystemIn();
            System.setIn(new ByteArrayInputStream(new byte[0]));
            return instance;
        }
    }

    static class SystemOut extends TestWatcher {
        ByteArrayOutputStream baos;
        BufferedReader reader;

        protected void starting(org.junit.runner.Description description) {
            baos = new ByteArrayOutputStream();
            System.setOut(new PrintStream(baos));
        }

        public String readMassage() throws IOException {
            String line = readLine();
            if (line == null) return null;
            return line.equals("> ") ? readMassage() : line.replace("> ", "");
        }

        public String readLine() throws IOException {
            if (reader == null) {
                reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
            }
            return reader.readLine();
        }
    }
}
