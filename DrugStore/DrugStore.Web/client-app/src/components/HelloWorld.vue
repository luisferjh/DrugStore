<template>
   <v-layout
    text-center
    wrap
  >
    <v-flex xs12>
      <v-img
        :src="require('../assets/logo.png')"
        class="my-3"
        contain
        height="200"
      ></v-img>
    </v-flex>

    <v-flex xs-12>
      <h1 class="display-2 font-weight-bold mb-3">
        Welcome to Vuetify
      </h1>
      <p class="subheading font-weight-regular">
        For help and collaboration with other Vuetify developers,
        <br>please join our online
        <a href="https://community.vuetifyjs.com" target="_blank">Discord Community</a>
      </p>
    </v-flex>

  </v-layout>
</template>

<script>
export default {
    name:'HelloWorld',
    data: () => ({  

    })
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->

