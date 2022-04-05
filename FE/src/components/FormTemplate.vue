<template>
  <div class="form">
    <FormKit v-if="isLoaded" type="form" @submit="handleSubmit">
      <FormKit type="text" label="Neved:" v-model="formValues.name" />
      <FormKit type="email" label="Email Cimed:" v-model="formValues.email" />
      <FormKit
        v-for="(field, index) in formData.questions"
        :key="index"
        :type="formData.type[index]"
        :label="'Question ' + (index + 1) + ' ' + field"
        :options="formData.possibleAnswers[index]"
        :validation="formData.validations[index]"
        v-model="formValues.answers[index]"
      ></FormKit>
      <FormKit
        type="textarea"
        label="Megjegyzesek:"
        v-model="formValues.comments"
      />
    </FormKit>
  </div>
</template>

<script>
export default {
  created() {
    this.GetForm();
  },
  data: () => ({
    formValues: {
      name: "",
      email: "",
      comments: "",
      answers: [],
    },
    isLoaded: false,
    formData: {},
  }),
  methods: {
    handleSubmit() {
      console.log(this.formValues);
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      this.prepareAnswers();
      var raw = JSON.stringify({
        user: this.formValues.name,
        email: this.formValues.email,
        comments: this.formValues.comments,
        formId: 1,
        answers: this.formValues.answers,
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      fetch(window.origin + "/api/SubmittedForms", requestOptions)
        .then((response) => response.text())
        .then((result) => console.log(result))
        .catch((error) => console.log("error", error));
    },
    prepareAnswers() {
      for (let index = 0; index < this.formValues.answers.length; index++) {
        var element = this.formValues.answers[index];
        if (typeof element == "object") {
          this.formValues.answers[index] = element.join();
        }
      }

      console.log(this.formValues);
    },
    GetForm() {
      console.log("Get Form called.");
      fetch(window.origin + "/api/Forms/1", {
        headers: {
          Accept: "text/plain",
        },
      })
        .then((response) => response.json())
        .then((data) => {
          console.log(), (this.isLoaded = true);
          this.PopulateForm(data);
          console.log(data);
        });
    },
    PopulateForm(data) {
      data.type = [];
      for (let index = 0; index < data.questions.length; index++) {
        const questionType = data.questionType[index];
        switch (questionType) {
          case 0:
            data.type.push("text");
            break;
          case 1:
            data.type.push("radio");
            data.possibleAnswers[index] =
              data.possibleAnswers[index].split(",");
            break;
          case 2:
            data.type.push("checkbox");
            data.possibleAnswers[index] =
              data.possibleAnswers[index].split(",");
            break;
          default:
            break;
        }
        if (data.validations[index]) {
          data.questions[index] += "*";
        }
      }
      this.formData = data;
    },
  },
};
</script>

<style>
.form {
  display: flex;
  justify-content: center;
}
</style>
