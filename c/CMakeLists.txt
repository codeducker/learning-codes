cmake_minimum_required(VERSION 3.26)
project(c_primer_plus_demo C)

set(CMAKE_C_STANDARD 99)
set(EXECUTABLE_OUTPUT_PATH ${PROJECT_BINARY_DIR})


# set(FILE_TXT "file.txt")

# add_custom_command(OUTPUT ${FILE_TXT}
  # COMMAND ${CMAKE_COMMAND} -E copy ${PROJECT_SOURCE_DIR}/question_08/${FILE_TXT} ${FILE_TXT}
# )
# add_custom_target(FILE_COPY ALL DEPENDS ${FILE_TXT})

# add_custom_command(
  # TARGET FILE_COPY PRE_BUILD
  # COMMAND echo  "execute a copy task"
# )
message("name ${EXECUTE_FILE_NAME}")
add_executable(c_primer_plus_demo ${EXECUTE_FILE_NAME})
