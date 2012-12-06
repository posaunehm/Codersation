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
	public SystemIn in = new SystemIn();

	@Test
	public void お金の投入はinsでする() throws Exception {
		in.writeLine("ins 100");
		in.writeLine("ins 10");

		Main.main();

		assertThat(out.readLine(), is("> "));
		assertThat(out.readLine(), is("money: 100 was received."));
		assertThat(out.readLine(), is("> "));
		assertThat(out.readLine(), is("money: 10 was received."));
	}

	@Test
	public void 変なお金をinsしたらば() throws Exception {
		in.writeLine("ins 123");

		Main.main();

		assertThat(out.readLine(), is("> "));
		assertThat(out.readLine(), is("! 123 is not available."));
	}

	static class SystemIn {
		StringBuilder sb = new StringBuilder();

		public void writeLine(String str) {
			sb.append(str);
			sb.append(System.lineSeparator());
			System.setIn(new ByteArrayInputStream(sb.toString().getBytes()));
		}
	}

	static class SystemOut extends TestWatcher {
		ByteArrayOutputStream baos;
		BufferedReader reader;

		protected void starting(org.junit.runner.Description description) {
			baos = new ByteArrayOutputStream();
			System.setOut(new PrintStream(baos));
		};

		public String readLine() throws IOException {
			if (reader == null) {
				reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
			}
			return reader.readLine();
		}
	};
}
