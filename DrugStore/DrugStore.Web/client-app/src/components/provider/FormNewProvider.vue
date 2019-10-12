<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Proveedor</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombre del Proveedor*" v-model="providerName" required></v-text-field>
          </v-col>
              
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Numero del documento" v-model="documentNumber" required></v-text-field>
          </v-col>
                  
          <v-col cols="12">
            <v-text-field label="Direccion" v-model="address" required></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Numero de telefono" v-model="phoneNumber" required></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Email" v-model="email" required></v-text-field>
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default { 
    data() {
      return {
        providerName:'',
        documentNumber:'',
        address:'',
        phoneNumber:'',
        email:''      
      }
    },
    methods: {
      save() {
        this.$emit('save',{            
            'providerName':this.providerName,
            'documentNumber':this.documentNumber, 
            'address':this.address,
            'phoneNumber':this.phoneNumber,
            'email':this.email
            })
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

