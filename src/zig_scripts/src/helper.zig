pub fn sum(a: i8, b: i8) i8 {
    return a + b;
}

const std = @import("std");

pub fn getTwoDigitNumber(input: []u8) !u16 {
    var stdout = std.io.getStdOut().writer();

    var firstDigit: u8 = 0;
    var lastDigit: u8 = 0;
    var isFirstDigitFound: bool = false;

    for (input) |byte| {
        if (std.ascii.isDigit(byte)) {
            if (!isFirstDigitFound) {
                firstDigit = byte;
                isFirstDigitFound = true;
            }
            lastDigit = byte;
        }
    }

    const twoDigitNumber: u16 = ((firstDigit - '0') * 10) + (lastDigit - '0');
    try stdout.print("Found two digit number: {}\n", .{twoDigitNumber});

    return twoDigitNumber;
}

pub fn getNumbers(input: []u8) !void {
    const allocator = std.heap.page_allocator;
    var stdout = std.io.getStdOut().writer();

    // Example string
    // const input = "9six6seven";

    // Buffer to hold the current number as a string
    var currentNumber: [20]u8 = undefined; // Large enough for most integers
    var currentLength: usize = 0;

    // ArrayList to hold the parsed integers
    var numbers = std.ArrayList(i32).init(allocator);
    defer numbers.deinit();

    // Iterate through each character in the string
    for (input) |char| {
        if (std.ascii.isDigit(char)) {
            // If the character is a digit, add it to the current number buffer
            currentNumber[currentLength] = char;
            currentLength += 1;
        } else {
            // If the character is not a digit, try to parse the current number (if any)
            if (currentLength > 0) {
                numbers.append(try std.fmt.parseInt(i32, currentNumber[0..currentLength], 10)) catch |errorInstance| {
                    try stdout.print("Error occured: {}\n", .{errorInstance});
                };
                currentLength = 0; // Reset buffer length
            }
        }
    }

    // Make sure to parse any number that was being collected at the end of the string
    if (currentLength > 0) {
        numbers.append(try std.fmt.parseInt(i32, currentNumber[0..currentLength], 10)) catch |errorInstance| {
            try stdout.print("Error occured: {}\n", .{errorInstance});
        };
        currentLength = 0; // Reset buffer length
    }

    // Output the parsed numbers
    for (numbers.items) |num| {
        try stdout.print("Parsed integer: {}\n", .{num});
    }
}

// pub fn main() !void {
//     std.debug.print("Hello, world!\n", .{});
//     const a = 5;
//     const b = 10;

//     const result = hp.sum(a, b);

//     std.debug.print("result: {}\n", .{result});
//     const stdin = std.io.getStdIn();

//     std.debug.print("Press Enter to exit...\n", .{});

//     var buffer: [10]u8 = undefined;
//     // var input: i64 = undefined;
//     // if (try stdin.readUntilDelimiterOrEof(buf[0..], '\n')) |user_input| {
//     //     input = std.fmt.parseInt(i64, user_input, 10);
//     // } else {
//     //     input = @as(i64, 0);
//     // }
//     // Read from stdin; this waits for the user to press Enter
//     _ = try stdin.read(buffer[0..]);

//     std.debug.print("Exiting.\n", .{});
// }

// pub fn main() !void {
//     // Prints to stderr (it's a shortcut based on `std.io.getStdErr()`)
//     std.debug.print("All your {s} are belong to us.\n", .{"codebase"});

//     // stdout is for the actual output of your application, for example if you
//     // are implementing gzip, then only the compressed bytes should be sent to
//     // stdout, not any debugging messages.
//     const stdout_file = std.io.getStdOut().writer();
//     var bw = std.io.bufferedWriter(stdout_file);
//     const stdout = bw.writer();

//     try stdout.print("Run `zig build test` to run the tests.\n", .{});

//     try bw.flush(); // don't forget to flush!
// }

// // build with `zig build-exe cimport.zig -lc -lraylib`
// const ray = @cImport({
//     @cInclude("raylib.h");
// });

// pub fn main() void {
//     const screenWidth = 800;
//     const screenHeight = 450;

//     ray.InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");
//     defer ray.CloseWindow();

//     ray.SetTargetFPS(60);

//     while (!ray.WindowShouldClose()) {
//         ray.BeginDrawing();
//         defer ray.EndDrawing();

//         ray.ClearBackground(ray.RAYWHITE);
//         ray.DrawText("Hello, World!", 190, 200, 20, ray.LIGHTGRAY);
//     }
// }
