import React, { useState } from 'react';
import { Button, Col, Form, Row } from 'react-bootstrap';
import { Field, FieldProps, Formik } from 'formik';
import {
  ChartValues,
  DefaultHistoricalDataRequest,
  GroupingValues,
  HistoricalDataRequestValidationSchema,
  IHistoricalDataRequest,
} from '../../model/HistoricalDataTypes';
// https://react-bootstrap.github.io/forms/validation/
const HistoricalData: React.FC = () => {
  const [formValues] = useState<IHistoricalDataRequest>(DefaultHistoricalDataRequest);

  return (
    <Formik
      validationSchema={HistoricalDataRequestValidationSchema}
      onSubmit={console.log}
      initialValues={formValues}
      validateOnChange={false}
    >
      {({ handleSubmit, handleChange, values, touched, errors }) => (
        <Form noValidate onSubmit={handleSubmit}>
          <Row className="mb-3 mt-5">
            <Form.Group as={Col} controlId="initialDate">
              <Form.Label>Initial date</Form.Label>
              <Form.Control
                type="date"
                name="initialDate"
                onChange={handleChange}
                isValid={touched.initialDate && !errors.initialDate}
                isInvalid={!!errors.initialDate}
              />
              <Form.Control.Feedback type="invalid">{errors.initialDate}</Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col} controlId="finalDate">
              <Form.Label>Final Date</Form.Label>
              <Form.Control
                type="date"
                name="finalDate"
                onChange={handleChange}
                isValid={touched.finalDate && !errors.finalDate}
                isInvalid={!!errors.finalDate}
              />
              <Form.Control.Feedback type="invalid">{errors.finalDate}</Form.Control.Feedback>
            </Form.Group>
          </Row>
          <Row className="mb-3">
            <Form.Group as={Col} controlId="chartView">
              <Form.Label>Chart view</Form.Label>
              <Field name="chartView">
                {({ field, form: { setFieldValue } }: FieldProps) => (
                  <>
                    <Form.Check
                      type="radio"
                      id="chartView-lines"
                      checked={values.chartView === ChartValues.Lines}
                      name="chartView"
                      label={ChartValues.Lines}
                      onChange={() => setFieldValue(field.name, ChartValues.Lines, false)}
                    />
                    <Form.Check
                      type="radio"
                      id="chartView-bars"
                      checked={values.chartView === ChartValues.Bars}
                      name="chartView"
                      label={ChartValues.Bars}
                      onChange={() => setFieldValue(field.name, ChartValues.Bars, false)}
                    />
                  </>
                )}
              </Field>
            </Form.Group>
            <Form.Group as={Col} controlId="grouping">
              <Form.Label>Grouping</Form.Label>
              <Field name="grouping">
                {({ field, form: { setFieldValue } }: FieldProps) => (
                  <Form.Select
                    className={'form-control'}
                    isValid={touched.grouping && !errors.grouping}
                    isInvalid={!!errors.grouping}
                    onChange={(option) => setFieldValue(field.name, option.target.value, false)}
                  >
                    <option value={undefined}>Open this select menu</option>
                    <option value={GroupingValues.Hours}>{GroupingValues.Hours}</option>
                    <option value={GroupingValues.Days}>{GroupingValues.Days}</option>
                    <option value={GroupingValues.Months}>{GroupingValues.Months}</option>
                  </Form.Select>
                )}
              </Field>
              <Form.Control.Feedback type="invalid">{errors.grouping}</Form.Control.Feedback>
            </Form.Group>
          </Row>
          <Row>
            <Col>
              <Button type="submit">Submit form</Button>
            </Col>
          </Row>
        </Form>
      )}
    </Formik>
  );
};

export default HistoricalData;
