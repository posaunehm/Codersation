package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.io.StringReader;

import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.TestWatcher;

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

		assertThat(out.readMesage(), is("money: 100 was received."));
		assertThat(out.readMesage(), is("money: 10 was received."));
	}

	@Test
	public void 変な金額をinsしたらエラーメッセージ() throws Exception {
		in.writeLine("ins 123");

		Main.main();

		assertThat(out.readMesage(), is("! 123 is not available."));
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
		};

		public String readMesage() throws IOException {
			String line = readLine();
			return line.equals("> ") ? readMesage() : line;
		}

		public String readLine() throws IOException {
			if (reader == null) {
				reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
			}
			return reader.readLine();
		}
	};
}
